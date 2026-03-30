using my_aspnet_project.Models;
using my_aspnet_project.Interfaces;
namespace my_aspnet_project.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();
    private int _currentId = 1;

    public IEnumerable<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public TaskItem Add(TaskItem item)
    {
        item.Id = _currentId++;
        _tasks.Add(item);
        return item;
    }
}