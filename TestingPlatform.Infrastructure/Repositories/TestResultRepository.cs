using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class TestResultRepository(AppDbContext appDbContext, IMapper mapper) : ITestResultRepository
    {


        private const int NOT_SCORED = 0;
        public Task<List<TestResultDto>> GetAllAsync()
        {
            var results = appDbContext.TestResults
                .Include(t => t.Attempt)
                .Select(t => new TestResultDto 
                {
                    Id = t.Id,
                    Passed = t.Passed,
                    TestId = t.TestId,
                    AttemptId = t.AttemptId,
                    StuidentId = t.StuidentId,
                    BestScore = t.Attempt.Score ?? NOT_SCORED

                })
                .ToListAsync();

            return results;
        }

        public Task<List<TestResultDto>> GetByStudentIdAsync(int studentId)
        {
            var results = appDbContext.TestResults
               .Include(t => t.Attempt)
               .Where(t => t.StuidentId == studentId)
               .Select(t => new TestResultDto
               {
                   Id = t.Id,
                   Passed = t.Passed,
                   TestId = t.TestId,
                   AttemptId = t.AttemptId,
                   StuidentId = t.StuidentId,
                   BestScore = t.Attempt.Score ?? NOT_SCORED

               })
               .ToListAsync();

                return results;
        }
    }
}
