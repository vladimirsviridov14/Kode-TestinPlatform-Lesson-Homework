using Microsoft.EntityFrameworkCore;

namespace TestingPlatform.Models
{

    [Index(nameof(Name), IsUnique = true)]
    public class Direction
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Group> Groups { get; set; }

    }
}

