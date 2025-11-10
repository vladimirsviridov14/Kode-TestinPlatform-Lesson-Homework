using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Enums;
using TestingPlatform.Models;
using TestingPlatform.Requests.Student;
using TestingPlatform.Respones.Student;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Students(IStudentRepository studentRepository, IUserRepository userRepository, IMapper mapper) : ControllerBase
    {
       

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<StudentRespones>>(students));
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest("Некорректный id");

            var student = await studentRepository.GetByIdAsync(id);

         

            return Ok(mapper.Map<StudentRespones>(student));
        }


        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest student)
        {
            var userDto = new UserDto
            {
                Email = student.Email,
                FirtsName = student.FirtsName,
                LastName = student.LastName,
                Login = student.Login,
                MiddleName = student.MiddleName,
                Password = student.Password,
                Role = UserRole.Student,


            };

            var userId = await userRepository.CreateAsync(userDto);

            var studentDto = new StudentDto
            {
                UserId = userId,
                Phone = student.Phone,
                VKProfileLink = student.VKProfileLink,
            };


            var studentId = await studentRepository.CreateAsync(studentDto);

            return StatusCode(StatusCodes.Status201Created, new { Id = studentId });



        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest student, [FromRoute] int id)
        {
          

            if (id <= 0)
                return BadRequest("Некорректный id");

         
            await studentRepository.UpdateAsync(mapper.Map<StudentDto>(student), id);



            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
           await studentRepository.DeleteAsync(id);
            return NoContent();
        }








    }

}
