using TestingPlatform.Enums;
using TestingPlatform.Respones.Student;

namespace TestingPlatform.Respones.Auth
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }

        public StudentRespones Student { get; set; }
    }
}
