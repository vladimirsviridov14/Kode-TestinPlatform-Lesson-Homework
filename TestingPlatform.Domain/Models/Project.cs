using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<Test> Groups { get; set; }

    }
}
