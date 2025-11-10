using TestingPlatform.Application.Dtos;
using TestingPlatform.Respones.Direction;
using TestingPlatform.Respones.Group;
using AutoMapper;
using TestingPlatform.Requests.Group;


namespace TestingPlatform.Mappings
{
    public class GroupProfile: Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupDto, GroupRespones>();
            CreateMap<CreateGroupRequest, GroupDto>()
                .ForMember(d => d.Direction, o => o.MapFrom(s => s.DirectionId > 0 ? new DirectionDto {Id = s.DirectionId } :null))
             .ForMember(d => d.Course, o => o.MapFrom(s => s.CourseId > 0 ? new CourseDto { Id = s.CourseId } : null))
             .ForMember(d => d.Project, o => o.MapFrom(s => s.ProjectId > 0 ? new ProjectDto { Id = s.ProjectId } : null));

            CreateMap<UpdateGroupRequest, GroupDto>()
               .ForMember(d => d.Direction, o => o.MapFrom(s => s.DirectionId > 0 ? new DirectionDto { Id = s.DirectionId } : null))
            .ForMember(d => d.Course, o => o.MapFrom(s => s.CourseId > 0 ? new CourseDto { Id = s.CourseId } : null))
            .ForMember(d => d.Project, o => o.MapFrom(s => s.ProjectId > 0 ? new ProjectDto { Id = s.ProjectId } : null));
        }
    }
}

