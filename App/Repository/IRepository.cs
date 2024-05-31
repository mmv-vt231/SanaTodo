using App.DTO;
using App.Models;

namespace App.Repository
{
    public interface IRepository
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<GetTasksDTO> GetTasks();
        void CreateTask(CreateTaskDTO task);
        void DeleteTask(int id);
        void CompleteTask(int id, bool completed);
    }
}
