using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Requests.Group;
using AutoMapper;
using TestingPlatform.Respones.Group;
namespace TestingPlatform.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Groups(IGroupRepository groupRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await groupRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<GroupRespones>>(groups));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroupById( int id)
        {
            var group = await groupRepository.GetByIdAsync(id);

            return Ok(mapper.Map<GroupRespones>(group));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest group)
        {
            var id = await groupRepository.CreateAsync(mapper.Map<GroupDto>(group));
            return StatusCode(StatusCodes.Status201Created, new { Id = id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequest group, [FromRoute] int id)
        {
            await groupRepository.UpdateAsync(mapper.Map<GroupDto>(group), id);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGroup( int id)
        {
            await groupRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
