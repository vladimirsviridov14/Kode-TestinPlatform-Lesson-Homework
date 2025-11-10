using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Respones.Direction;

namespace TestingPlatform.Mappings


{
    public class DirectionProfile: Profile
    {
        public DirectionProfile()
        {
            CreateMap<DirectionDto, DirectionRespones>();
        }
       
    }
}
