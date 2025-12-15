using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Requests.Attempt;

namespace TestingPlatform.Mappings
{
    public class AttemptProfile : Profile
    {

        CreateMap<CreateAttemptRequest, AttemptDto>();
        CreateMap<CreateAttemptRequest, AttemptDto>();

    }
}
