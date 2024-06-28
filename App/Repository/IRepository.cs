using App.DTO;
using App.Models;

namespace App.Repository
{
    public interface IRepository
    {
        IEnumerable<GetTasksDTO> GetTasksWithCategory();
        IEnumerable<Models.Task> GetTasks();
        Models.Task CreateTask(CreateTaskDTO task);
        int DeleteTask(int id);
        int CompleteTask(int id, bool completed);
        IEnumerable<Category> GetCategories();
        Task<IDictionary<int, Category>> GetCategoriesById(IEnumerable<int> ids, CancellationToken cancellationToken);
        Category GetCategory(int id);
    }
}
