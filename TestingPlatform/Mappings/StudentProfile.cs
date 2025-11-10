using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Requests.Student;
using TestingPlatform.Respones.Project;
using TestingPlatform.Respones.Student;

namespace TestingPlatform.Mappings
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, StudentRespones>()
                .ForMember(d => d.Login, m => m.MapFrom(s => s.User.Login))
                .ForMember(d => d.Email, m => m.MapFrom(s => s.User.Email))
                .ForMember(d => d.FirtsName, m => m.MapFrom(s => s.User.FirtsName))
                .ForMember(d => d.MiddleName, m => m.MapFrom(s => s.User.MiddleName))
                .ForMember(d => d.LastName, m => m.MapFrom(s => s.User.LastName))
                .ForMember(d => d.Phone, m => m.MapFrom(s => s.Phone))
                .ForMember(d => d.VKProfileLink, m => m.MapFrom(s => s.VKProfileLink));

            CreateMap<UpdateStudentRequest, StudentDto>();
              


        }

    }
}
