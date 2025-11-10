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
    public  class TestProfile: Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestDto>().ReverseMap();
            CreateMap<TestDto, Test>()
                .ForMember(d => d.Questions, o => o.Ignore())
                .ForMember(d => d.Students, o => o.Ignore())
                .ForMember(d => d.Projects, o => o.Ignore())
                .ForMember(d => d.Courses, o => o.Ignore())
                .ForMember(d => d.Groups, o => o.Ignore())
                .ForMember(d => d.Directions, o => o.Ignore());
               

        }
    }
}
