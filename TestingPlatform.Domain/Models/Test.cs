using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestingPlatform.Enums;

namespace TestingPlatform.Models
{
    public class Test
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

       public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsRepeatable { get; set; }
        
        public  TestType Type { get; set; }

        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset PublishedAt { get; set; }

        [Required]
        public DateTimeOffset Deadlinen { get; set; }
        
        public int? DurationMinutes { get; set; }

        [DefaultValue(false)]
        public bool IsPublic { get; set; }
        
        public int? PassingScore { get; set; }
        
        public int? MaxAttempts { get; set; }

        //навигация 

        public List<Group> Groups { get; set; }

        public List<Course> Courses { get; set; }

        public List<Project> Projects { get; set; }

        public List<Direction> Directions { get; set; }

        public List<Student> Students { get; set; }
        public List<Student> Questions { get; set; }


        
    }
}
