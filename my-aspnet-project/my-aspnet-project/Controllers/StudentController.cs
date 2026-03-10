using Microsoft.AspNetCore.Mvc;
using my_aspnet_project.Models;

namespace my_aspnet_project.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class StudentController : ControllerBase
{
    private static List<StudentModel> students = new()
    {
        new StudentModel
        {
            Id = 1,
            Name = "Stas",
            Age = 20,
            EnrollmentYear = 2023,
            Courses = new List<Course>
            {
                new Course { Id = 1, Name = "Math" },
                new Course { Id = 2, Name = "Programming" }
            }
        },
        new StudentModel
        {
            Id = 2,
            Name = "Prokhor",
            Age = 19,
            EnrollmentYear = 2024,
            Courses = new List<Course>
            {
                new Course { Id = 1, Name = "Databases" },
                new Course { Id = 2, Name = "Programming" }
            }
        }
    };
    
    [HttpGet]
    public IActionResult GetAll(int page = 1, int pageSize = 10)
    {
        var result = students
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);
        
        if (student == null)
            return NotFound();
        
        return Ok($"Student {id}");
    }
    
    [HttpGet("name/{name}")]
    public IActionResult GetByName(string name)
    {
        var result = students
            .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(result);
    }
    
    [HttpGet("year/{year:int}")]
    public IActionResult GetByYear(int year)
    {
        var result = students
            .Where(s => s.EnrollmentYear == year)
            .ToList();

        return Ok(result);
    }
    
    [HttpGet("{id:int}/courses")]
    public IActionResult GetStudentCourses(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student == null)
            return NotFound();

        return Ok(student.Courses);
    }
    
    [HttpPost]
    public IActionResult Create(StudentModel student)
    {
        student.Id = students.Max(s => s.Id) + 1;
        students.Add(student);

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, StudentModel updatedStudent)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student == null)
            return NotFound();

        student.Name = updatedStudent.Name;
        student.Age = updatedStudent.Age;
        student.EnrollmentYear = updatedStudent.EnrollmentYear;
        student.Courses = updatedStudent.Courses;

        return Ok(student);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student == null)
            return NotFound();

        students.Remove(student);

        return NoContent();
    }
}