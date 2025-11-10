using TestingPlatform.Respones.Course;
using TestingPlatform.Respones.Direction;
using TestingPlatform.Respones.Project;

namespace TestingPlatform.Respones.Group
{
    public class GroupRespones: BaseRespones

    {
        public DirectionRespones Direction { get; set; }

        public CourseRespones Course { get; set; }

        public ProjectRespones Project { get; set; }
    }
}
