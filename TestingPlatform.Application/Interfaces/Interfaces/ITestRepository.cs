using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestDto>> GetAllAsync(bool? isPublic, List<int> groupIds, List<int> studentsIds);
        Task<IEnumerable<TestDto>> GetAllForStudentsAsync(int studentId);
        Task<IEnumerable<TestDto>> GetByIdAsync(int id);
        Task<int> CreateAsync(TestDto testDto);

        Task UpdateAsync(TestDto testDto);
        Task DeleteAsync(int id);

        Task<IEnumerable<TestDto>> GetTopRecentAsync(int count = 5);
    }
}
