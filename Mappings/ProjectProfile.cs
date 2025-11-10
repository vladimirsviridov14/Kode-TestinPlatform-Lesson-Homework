using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Respones.Group;
using TestingPlatform.Respones.Project;
namespace TestingPlatform.Mappings
{
    public class ProjectProfile: Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, ProjectRespones>();
        }
    }
}
