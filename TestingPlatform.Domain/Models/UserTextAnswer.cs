using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Models
{
    public class UserTextAnswer
    {
        public int Id {  get; set; }
        public string TextAnswer { get; set; }

        [Required]
        public int UserAttemptAnswerId { get; set; }

        //навигация
        public UserAttemptAnswer UserAttemptAnswer { get; set; }
    }
}
