using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Models;

namespace TestingPlatform.Infrastructure.Mappings
{
    internal class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, Student>()
                 .ForMember(d => d.User, m => m.Ignore());


            CreateMap<Student, StudentDto>()
                .ForMember(d => d.User, m => m.MapFrom(s => s.User));


        }
    }
}
