using TestingPlatform.Application.Dtos;
using AutoMapper;
using TestingPlatform.Respones.Course;

namespace TestingPlatform.Mappings
{
    public class CourseProfile: Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDto, CourseRespones>();
             
        }
    }
}
