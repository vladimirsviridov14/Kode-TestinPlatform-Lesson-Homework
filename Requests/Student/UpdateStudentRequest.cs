using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Requests.Student
{
    public class UpdateStudentRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirtsName { get; set; }

        [Required]
        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }


        [Required]
        public string VKProfileLink { get; set; }
    }
}
