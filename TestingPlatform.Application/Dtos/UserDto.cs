using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TestingPlatform.Enums;
using TestingPlatform.Models;

namespace TestingPlatform.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string FirtsName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }

     

    }
}