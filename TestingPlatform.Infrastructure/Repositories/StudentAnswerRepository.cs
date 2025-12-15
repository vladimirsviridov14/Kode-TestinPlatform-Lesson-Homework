using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Enums;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class StudentAnswerRepository(AppDbContext appDbContext): IStudentAnswerRepository
    {
        public async Task CreateAsync(UserAttemptAnswer userAttemptAnswerDto)
        {
            var attempt = await appDbContext.Attempts
                .Include(a => a.UserAttemptAnswers)
                .FirstOrDefaultAsync(a => a.Id == userAttemptAnswerDto.AttemptId);

            if (attempt != null)
                throw new EntityNotFoundException();

            if(attempt.SubmittedAt != null)
                throw new InvalidOperationException("Нельзя изменить ответ в уже отправленной попытке.");
            
            var question = await appDbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == userAttemptAnswerDto.QuestionId);

            if (question == null)
                throw new EntityNotFoundException("Вопрос не найден.");

            var userAttemptAnswer = new UserAttemptAnswer
            {
               AttemptId = attempt.Id,
                QuestionId = question.Id,
                UserSelectedOptions = new List<UserSelectedOption>(),
                UserTextAnswer = null,
                IsCorrect = false,
                ScoreAwarded = 0,

            };

            switch (question.AnswerType) 
            {
                case AnswerType.SingleChoice:
                    {
                        var selected = userAttemptAnswerDto.UserSelectedOptions?.FirstOrDefault();

                        if (selected == 0 || selected is null)
                            throw new InvalidOperationException("Не выбран ни один вариант ответа.");

                        var selectedAnswerEntity = question.Answers
                            .FirstOrDefault(a => a.Id == selected);

                        if (selectedAnswerEntity == null)
                            throw new EntityNotFoundException("Выбранный вариант ответа не найден.");

                        userAttemptAnswer.IsCorrect = selectedAnswerEntity.IsCorrect;

                        if (question.IsScoring)
                        {
                            var max = question.Maxscore ?? 1;
                            userAttemptAnswer.ScoreAwarded = selectedAnswerEntity.IsCorrect ? max : 0;
                        }
                        else 
                        {
                            userAttemptAnswer.ScoreAwarded = 0;
                        }

                        userAttemptAnswer.UserSelectedOptions.Add(new UserSelectedOption
                        {
                            AnswerId = selected.Value,
                        });

                        break;
                    }
                case AnswerType.MultipleChoice:
                    {
                        var selectedIds = userAttemptAnswerDto.UserSelectedOptions ?? new List<int>();

                        if(selectedIds.Count == 0)
                            throw new InvalidOperationException("Не выбран ни один вариант ответа.");

                        var correctAnswers = question.Answers.Where(a => a.IsCorrect).Select(a => a.Id).ToHashSet();

                        if (selectedIds.Any(id => !allAnswersIds.Contains(id)))
                            throw new EntityNotFoundException("Один или несколько выбранных вариантов ответа не найдены.");


                        var selectedSet = selectedIds.ToHashSet();
                        var isExactMatch = selectedSet.SetEquals(correctAnswers);

                        userAttemptAnswer.IsCorrect = isExactMatch;

                        if (question.IsScoring)
                        {
                            var max = question.Maxscore ?? 1;
                            userAttemptAnswer.ScoreAwarded = isExactMatch ? max : 0;
                        }
                        else
                        {
                            userAttemptAnswer.ScoreAwarded = 0;
                        }

                        foreach (var aid in selectedIds) 
                        {
                            userAttemptAnswer.UserSelectedOptions.Add(new UserSelectedOption
                            {
                                AnswerId = aid,
                            });
                        }

                        break;









                    }
                case AnswerType.Text:
                    {
                        break;
                    }
            }

            await appDbContext.AddAsync(userAttemptAnswer);
            await appDbContext.SaveChangesAsync();






        }


    }
}
