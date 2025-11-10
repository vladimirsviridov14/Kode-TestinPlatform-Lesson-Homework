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
  
        public class GroupProfile : Profile
        {
            public GroupProfile()
            {
                CreateMap<Group, GroupDto>();
                CreateMap<GroupDto, Group>()
                    .ForMember(x => x.Course, o => o.Ignore())
                    .ForMember(x => x.Project, o => o.Ignore())
                    .ForMember(x => x.Direction, o => o.Ignore());
            }
        }

    
}
