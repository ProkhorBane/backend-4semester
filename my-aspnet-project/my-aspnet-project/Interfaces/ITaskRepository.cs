using my_aspnet_project.Models;
namespace my_aspnet_project.Interfaces;

public interface ITaskRepository
{
    IEnumerable<TaskItem> GetAll();
    TaskItem GetById(int id);
    TaskItem Add(TaskItem item);
}