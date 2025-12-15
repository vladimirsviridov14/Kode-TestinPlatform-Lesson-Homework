using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlatform.Application.Dtos
{
    public class UserAttemptAnswer
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public int AttemptId { get; set; }

        public int QuestionId { get; set; }

        public List<int> UserSelectedOptions { get; set; }
        public string? UserTextAnswer { get; set; }
    }
}
