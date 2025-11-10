using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Answers : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents() => Ok("Список студентов"); // 200

        [HttpGet("{id}")]

        public IActionResult GetAnswerById(int id)
        {
            if (id == 1)
                return Ok("Ответ на вопрос 1");
            return NotFound();
        }

        [HttpGet("by-answer/{answerId}")]
        public IActionResult GetAnswerByTestId(int answerId) =>
       Ok($"Ответы для теста {answerId}");
        [HttpPost]
        public IActionResult CreateAnswer() => Created("/api/answer/1",
        "Ответ создан");
        [HttpPut("{id}")]
        public IActionResult UpdateAnswer(int id) => NoContent();
        [HttpDelete("{id}")]
        public IActionResult DeleteAnswer(int id) => NoContent();




    }
}
