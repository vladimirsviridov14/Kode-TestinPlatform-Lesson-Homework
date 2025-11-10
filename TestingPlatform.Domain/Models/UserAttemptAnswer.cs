using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Models
{
    public class UserAttemptAnswer
    {
        public int Id { get; set; } 

        public bool IsCorrect   { get; set; }

        [Range(0, 30)]
        public int ScoreAwarded { get; set; }

        [Required]
        public int AttemprId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        //навигация
        public Attempt Attempt { get; set; }

        public Question Question { get; set; }

        public List<UserSelectedOption>? UserSelectedOptions { get; set; }

        public UserTextAnswer? UserTextAnswer {  get; set; }

    }
}
