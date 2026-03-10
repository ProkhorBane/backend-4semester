using System.ComponentModel.DataAnnotations;

namespace my_aspnet_project.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
public class StudentModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public int EnrollmentYear { get; set; }
    public List<Course> Courses { get; set; } = new();
}