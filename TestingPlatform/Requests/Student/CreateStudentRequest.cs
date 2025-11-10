using System.ComponentModel.DataAnnotations;
using TestingPlatform.Enums;

namespace TestingPlatform.Requests.Student
{
    public class CreateStudentRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

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
