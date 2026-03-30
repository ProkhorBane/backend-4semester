using my_aspnet_project.Models;
using my_aspnet_project.DTO;
using my_aspnet_project.Interfaces;
using my_aspnet_project.Models;
namespace my_aspnet_project.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<TaskDto> GetAll()
    {
        return _repository.GetAll()
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Name = t.Name,
                CreatedAt = t.CreatedAt
            });
    }

    public TaskDto GetById(int id)
    {
        var task = _repository.GetById(id);

        if (task == null)
            return null;

        return new TaskDto
        {
            Id = task.Id,
            Name = task.Name,
            CreatedAt = task.CreatedAt
        };
    }

    public TaskDto Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow
        };

        var created = _repository.Add(task);

        return new TaskDto
        {
            Id = created.Id,
            Name = created.Name,
            CreatedAt = created.CreatedAt
        };
    }
}