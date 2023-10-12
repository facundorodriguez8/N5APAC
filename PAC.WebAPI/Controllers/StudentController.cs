using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAC.Domain;
using PAC.IBusinessLogic;
using PAC.WebAPI.Filters;

namespace PAC.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentdLogic _studentLogic;
        public StudentController(IStudentdLogic logic)
        {
            _studentLogic = logic;
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        //Aca deberia ir un StudenCreateRequest pero no lo hice por tiempo
        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student received)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdStudent = _studentLogic.InsertStudents(received);

            return CreatedAtAction(nameof(CreateStudent), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpGet]
        public IActionResult GetStudents(FromQuery] int? age)
        {
            var result = _studentLogic.GetStudents();
            if (age.HasValue)
            {
                students = students.Where(s => s.Age == age);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById([FromRoute] Guid id)
        {
            _studentLogic.GetStudentById(id);
            return Ok();
        }

    }
}
