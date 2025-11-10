using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Models
{
    public class Attempt
    {
        public int id {  get; set; }

        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTimeOffset? StartedAt { get; set; }

        public DateTimeOffset SubmittedAt { get; set; }

        public int? Score { get; set; }

        [Required]
        public int TestId { get; set; }

        [Required]
        public int StudentId { get; set; }

        //навигация
        public Test Test { get; set; }

        public Student Student { get; set; }

        public List<UserAttemptAnswer> UserAttemptAnswers { get; set; }
    }
}
