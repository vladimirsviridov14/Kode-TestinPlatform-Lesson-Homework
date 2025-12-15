using AutoMapper;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthRequest, UserLoginDto>();
            CreateMap<UserDto, AuthResponse>();
        }
    }
}
