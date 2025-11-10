using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TestingPlatform.Enums;

namespace TestingPlatform.Models
{
    [Index(nameof(Login), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        
        public string Login { get; set; }

        
        public string PasswordHash {  get; set; }

        
        public string Email {  get; set; }

     
        public string FirtsName { get; set; }

        public string? MiddleName { get; set; }

      
        public string LastName { get; set; }

        public UserRole Role {  get; set; }

       

    }
}
