using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Enums;
using TestingPlatform.Models;

namespace TestingPlatform.Application.Dtos
{
    public class TestDto
    {
        public int Id { get; set; }

      
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRepeatable { get; set; }

        public TestType Type { get; set; }

      
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset PublishedAt { get; set; }


        public DateTimeOffset Deadlinen { get; set; }

        public int? DurationMinutes { get; set; }

      
        public bool IsPublic { get; set; }

        public int? PassingScore { get; set; }

        public int? MaxAttempts { get; set; }

     

        public List<Group> Groups { get; set; }

        public List<Course> Courses { get; set; }

        public List<Project> Projects { get; set; }

        public List<Direction> Directions { get; set; }

        public List<Student> Students { get; set; }

      
    }
}
