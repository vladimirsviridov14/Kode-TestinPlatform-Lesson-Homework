using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IStudentAnswerRepository
    {
        Task CreateAsync(UserAttemptAnswer userAttemptQAnswerDto);
    }
}
