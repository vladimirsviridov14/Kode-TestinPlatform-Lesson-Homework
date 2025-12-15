using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Constans;
using TestingPlatform.Extensions;
using TestingPlatform.Requests.Attempt;

namespace TestingPlatform.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Attempt(IAttemptRepository attemptRepository, IMapper mapper) : ControllerBase

    {

        [HttpPost]
      public async Task<IActionResult> CreateAttempt(CreateAttemptRequest attempt)
        {
         var studentId = HttpContext.TryGetUserId();

            var attemptDto = mapper.Map<AttemptDto>(attempt);
            attemptDto.StudentId = studentId;


            var attemptId = await attemptRepository.CreateAsync(attemptDto);
           
            return StatusCode(StatusCodes.Status201Created, new { Id = attemptId});
        }


        [HttpPut]

        public async Task<IActionResult> UpdateAttemptAsync(CreateAttemptRequest attempt)
        {
            var studentId = HttpContext.TryGetUserId();

            var attemptDto = mapper.Map<AttemptDto>(attempt);
            attemptDto.StudentId = studentId;

            await attemptRepository.UpdateAsync(attemptDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
