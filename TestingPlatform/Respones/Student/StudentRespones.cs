using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Respones.Student
{
    public class StudentRespones

    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirtsName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string VKProfileLink { get; set; }

    }
}
