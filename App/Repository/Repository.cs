using App.Database;
using App.DTO;
using App.Models;
using Dapper;

namespace App.Repository
{
    public class Repository : IRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public Repository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void CreateTask(CreateTaskDTO task)
        {
            using var connection = _connectionFactory.Create();

            connection.Execute("INSERT INTO Tasks (Text, Completed, EndDate, CategoryId) VALUES (@Text, 'false', @EndDate, @CategoryId)", task);
        }

        public void DeleteTask(int id)
        {
            using var connection = _connectionFactory.Create();

            connection.Execute(
                "DELETE FROM Tasks WHERE Id = @Id", 
                new { Id = id }
            );
        }

        public void CompleteTask(int id, bool completed)
        {
            using var connection = _connectionFactory.Create();

            connection.Execute(
                "UPDATE Tasks SET Completed = @Completed WHERE Id = @Id", 
                new {
                    Id = id, 
                    Completed = !completed
                }
            );
        }

        public IEnumerable<Category> GetCategories()
        {
            using var connection = _connectionFactory.Create();

            var categories = connection.Query<Category>("SELECT * FROM Categories");

            return categories;
        }

        public IEnumerable<GetTasksDTO> GetTasks()
        {
            using var connection = _connectionFactory.Create();
            
            var tasks = connection.Query<GetTasksDTO>(
                "SELECT Tasks.Id, Tasks.Text, Tasks.Completed, Tasks.EndDate, Categories.Name AS Category " +
                "FROM Tasks " +
                "INNER JOIN Categories ON Tasks.CategoryId = Categories.Id");

            return tasks;
        }

    }
}
