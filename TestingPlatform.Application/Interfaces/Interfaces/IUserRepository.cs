using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllAsync();

        Task<UserDto> GetByIdAsync(int id);

        Task<int> CreateAsync(UserDto userDto);

        Task UpdateAsync(UserDto userDto);

        Task DeleteAsync(int id);
    }
}
