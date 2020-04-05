using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDZjazd2.DatabaseConnectors;
using APBDZjazd2.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBDZjazd2.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentDbService _studentDbService;

        public StudentsController(IStudentDbService studentDbService)
        {
            _studentDbService = studentDbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_studentDbService.GetStudents());
        }

        [HttpGet("enrollments/{id}")]
        public IActionResult GetEnrollments([FromRoute] int id)
        {
            return Ok(_studentDbService.GetEnrollments(id));
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById([FromRoute] int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Majewski");
            }
            else if (id == 3)
            {
                return Ok("Andrzejewski");
            }
            return NotFound($"Nie znaleziono studenta o id {id}!");
        }

        [HttpPost()]
        public IActionResult AddStudent([FromBody] Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromRoute] int id, [FromBody] Student student)
        {
            // update w bazie o ile by była
            return Ok($"Aktualizacja studenta o id {id} została ukończona. Nowe imie i nazwisko użytkownika to {student.FirstName} {student.LastName}.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            // kasujemy uzytkownika z bazy
            return Ok($"Usuwanie użytkownika o id {id} z bazy zostało ukończone.");
        }
    }
}