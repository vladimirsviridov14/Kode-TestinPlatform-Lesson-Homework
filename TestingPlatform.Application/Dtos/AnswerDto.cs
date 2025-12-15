using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlatform.Application.Dtos
{
    public class AnswerDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Флаг правильного ответа
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Связь с конкретным вопросом
        /// </summary>
        public int QuestionId { get; set; }
    }
}
