using Microsoft.AspNetCore.Mvc;
using my_aspnet_project.DTO;
using my_aspnet_project.Interfaces;

namespace my_aspnet_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<TaskDto>> GetAll()
    {
        return Ok(_service.GetAll());
    }
    
    [HttpGet("{id}")]
    public ActionResult<TaskDto> GetById(int id)
    {
        var task = _service.GetById(id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public ActionResult<TaskDto> Create(CreateTaskDto dto)
    {
        var created = _service.Create(dto);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}