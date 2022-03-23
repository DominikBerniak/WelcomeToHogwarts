using Microsoft.AspNetCore.Mvc;
using WelcomeToHogwarts.Persistance.DataTransferObjects;
using WelcomeToHogwarts.Persistance.Entities;
using WelcomeToHogwarts.Services.Interfaces;

namespace WelcomeToHogwarts.Controllers
{
    [ApiController, Route("/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var students = await _service.GetAllStudents();
            if (students.Count == 0)
            {
                return NotFound("No students in Hogwarts");
            }
            return Ok(students);
        }

        [HttpGet("{studentName}")]
        public async Task<ActionResult<Student>> GetStudentByName(string studentName)
        {
            var student = await _service.GetStudentByName(studentName);
            if (student is null)
            {
                return NotFound("No such student");
            }
            return Ok(student);
        }

        [HttpPut("assign-student")]
        public async Task<IActionResult> AssignStudentToRoom(AssignStudentDto data)
        {
            var assingStatus = await _service.AssignStudentToRoom(data);
            if (assingStatus.Status == Status.NotFound)
            {
                return NotFound(assingStatus.StatusMessage);
            }
            if (assingStatus.Status == Status.BadRequest)
            {
                return BadRequest(assingStatus.StatusMessage);
            }
            return Ok(assingStatus.StatusMessage);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            var createdStudent = await _service.AddStudent(student);
            return Created($"/student/{createdStudent.Name}", createdStudent);

        }
    }
}
