
using TestingPlatform.Application.Dtos;
using TestingPlatform.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<GroupDto>> GetAllAsync();
        Task<GroupDto> GetByIdAsync(int id);
        Task<int> CreateAsync(GroupDto group);   
        Task UpdateAsync(GroupDto group, int id);
        Task DeleteAsync(int id);
    }
}
