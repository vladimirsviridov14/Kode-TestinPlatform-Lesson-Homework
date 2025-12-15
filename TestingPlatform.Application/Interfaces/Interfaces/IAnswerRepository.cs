using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IAnswerRepository
    {
        /// <summary>
        /// Создать новый ответ.
        /// </summary>
        /// <param name="answerDto">Модель создания нового ответа.</param>
        /// <returns>Идентификатор нового ответа.</returns>
        Task<int> CreateAsync(AnswerDto answerDto);

        /// <summary>
        /// Обновить информацию об ответе.
        /// </summary>
        /// <param name="answerDto">Модель обновления ответа.</param>
        Task UpdateAsync(AnswerDto answerDto);

        Task DeleteAsync(int answerId);
    }
}
