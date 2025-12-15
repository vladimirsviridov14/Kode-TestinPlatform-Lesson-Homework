using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IAttemptRepository
    {
        Task<int> CreateAsync(AttemptDto attemptDto); 
        Task<int> UpdateAsync(AttemptDto attemptDto); 

    }
}
