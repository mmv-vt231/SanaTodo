using App.DTO;
using App.Models;

namespace App.Repository
{
    public interface IRepository
    {
        IEnumerable<GetTasksDTO> GetTasksWithCategory();
        IEnumerable<Models.Task> GetTasks();
        int CreateTask(CreateTaskDTO task);
        int DeleteTask(int id);
        int CompleteTask(int id, bool completed);
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
    }
}
