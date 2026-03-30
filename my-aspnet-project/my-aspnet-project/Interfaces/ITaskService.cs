using my_aspnet_project.DTO;
namespace my_aspnet_project.Interfaces;

public interface ITaskService
{
    IEnumerable<TaskDto> GetAll();
    TaskDto GetById(int id);
    TaskDto Create(CreateTaskDto dto);
}