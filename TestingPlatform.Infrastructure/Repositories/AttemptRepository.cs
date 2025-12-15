using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Exceptions;
using TestingPlatform.Models;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class AttemptRepository(AppDbContext appDbContext, IMapper mapper, ITestRepository testRepository) : IAttemptRepository
    {
        public async Task<int> CreateAsync(AttemptDto attemptDto)
        {
            var test = await testRepository.GetByIdAsync(attemptDto.TestId);

            if (test is null)
                throw new EntityNotFoundException("Test not found");

            var student = appDbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == attemptDto.StudentId);

            if (student is null)
                throw new EntityNotFoundException("Student not found");

            if(!test.IsPublic)
                throw new InvalidOperationException("Cannot create attempt for a non-public test");

            var availableTests = await testRepository.GetAllForStudentsAsync(attemptDto.StudentId);

            if (!availableTests.All(t => t.Id != attemptDto.TestId))
                throw new InvalidOperationException("Доступ запрещен");

            var attempt = mapper.Map<Attempt>(attemptDto);

            if (test.IsRepeatable && test.MaxAttempts is null)
                return await CreateAsync(attemptDto);

            var lastAttempts = await appDbContext.Attempts
               
                .Where(a => a.StudentId == attemptDto.StudentId && a.TestId == attemptDto.TestId)
                .ToListAsync();

            var inProgress = lastAttempts
                .FirstOrDefault(a => a.SubmittedAt == null);

            if (inProgress != null)
            {
                if (test.DurationMinutes.HasValue)
                {
                    var expriesAt = inProgress.StartedAt.AddMinutes(test.DurationMinutes.Value);
                    if(DateTimeOffset.UtcNow < expriesAt)
                        throw new InvalidOperationException("Есть незавершенная попытка, время выполнения еще не истекло");
                }

                else
                {
                    throw new InvalidOperationException("Есть незавершенная попытка. Тест не имеет ограничения по времени, поэтому новую попытку начать нельзя");
                }
            }

            if (!test.IsRepeatable && lastAttempts.Count > 0)
                throw new InvalidOperationException("Тест не является повторяемым, новая попытка создать нельзя");

            if(test.IsRepeatable && lastAttempts.Count > test.MaxAttempts)
                throw new InvalidOperationException("Превышено максимальное количество попыток для данного теста");

            return await CreateAsync(attemptDto);



        }

        private async Task<int> CreateAsync(Attempt attempt)
        {
           attempt.StartedAt = DateTimel.Now;
            attempt.Score = 0;
            var attempt = await  appDbContext.AddAsync(attempt);
            await appDbContext.SaveChangesAsync();

                return attempt.Entity.Id;
        }

        public async Task<int> UpdateAsync(AttemptDto attemptDto)
        {
           var attempt = await appDbContext.Attempts
                .Include(a => a.UserAttemptAnswers)
                .FirstOrDefaultAsync(a => a.Id == attemptDto.Id);

            if(attempt is null)
                throw new EntityNotFoundException("попытка не найдена");

            if(attempt.SubmittedAt != null)
                throw new InvalidOperationException("попытка уже завершена и не может быть изменена");

            attempt.SubmittedAt = DateTime.Now;

            var score = attempt.UserAttemptAnswers.Sun(ua => ua.ScoreAwarded);
            attempt.Score = score;

            var test = await testRepository.GetByIdAsync(attempt.TestId);

            var testResult = await appDbContext.TestResults
                .Include(tr => tr.Attempt)
                .FirstOrDefaultAsync(tr => tr.TestId == .Attempt.TestId);

            if (testResult == null)
            {
                var newTestResult = new TestResult
                {
                    AttemptId = attempt.Id,
                    StudentId = attempt.StudentId,
                    TestId = attempt.TestId,
                    Passed = test.PassingScore == null || test.PassingScore <= attempt.Score,


                };
                await appDbContext.TestResults.AddAsync(newTestResult);
            }
            else 
            {
                if (testResult.Attempt.Score < attempt.Score) 
                {
                    testResult.AttemptId = attempt.Id;
                    testResult.Passed = test.PassingScore == null || test.PassingScore <= attempt.Score;
                }

            }

            await appDbContext.SaveChangesAsync();


        }
    }
}
