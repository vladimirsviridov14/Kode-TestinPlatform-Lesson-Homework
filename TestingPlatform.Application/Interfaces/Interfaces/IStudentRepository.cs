using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task<int> CreateAsync(StudentDto studentDto);
        Task UpdateAsync(StudentDto studentDto, int id);
        Task DeleteAsync(int id);

    }
}
