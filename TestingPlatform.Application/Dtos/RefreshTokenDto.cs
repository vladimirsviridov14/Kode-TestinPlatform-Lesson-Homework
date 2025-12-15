using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlatform.Application.Dtos
{
    public class RefreshTokenDto
    {
        public UserDto user { get; set; }
        public DateTime expires { get; set; }
    }
}
