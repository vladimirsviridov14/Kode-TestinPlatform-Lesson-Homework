using System;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace TestingPlatform.Models
{
    public class Student
    {
        public object Tests;

        public int Id {get; set; }
        
       
        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

       
        [Required]
        public string VKProfileLink { get; set; }

        
        [Required]
        public int UserId {  get; set; }
      
       
        public User User { get; set; }
        
        
  




    }
}
