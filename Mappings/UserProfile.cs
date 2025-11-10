using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Models;
using TestingPlatform.Requests.Student;
using TestingPlatform.Respones.Student;

namespace TestingPlatform.Mappings
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
             CreateMap<User, UserDto>();
             CreateMap<UserDto, User>()
            .ForMember(d => d.PasswordHash, o => o.MapFrom(u => u.Password));
            
        }
}
}











