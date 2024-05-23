using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecordDb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // Get: https://localhost:1234/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = { "Alan", "James", "Charley", "Ethan" };

            return Ok(studentNames);
        }
    }
}
