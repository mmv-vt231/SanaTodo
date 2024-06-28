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

        public IEnumerable<GetTasksDTO> GetTasksWithCategory()
        {
            using var connection = _connectionFactory.Create();

            var tasks = connection.Query<GetTasksDTO>(@"
                SELECT Tasks.Id, Tasks.Text, Tasks.Completed, Tasks.EndDate, Categories.Name AS Category
                FROM Tasks
                JOIN Categories ON Tasks.CategoryId = Categories.Id");

            return tasks;
        }

        public IEnumerable<Models.Task> GetTasks()
        {
            using var connection = _connectionFactory.Create();

            var tasks = connection.Query<Models.Task>(@"SELECT * FROM Tasks");

            return tasks;
        }

        public Models.Task CreateTask(CreateTaskDTO task)
        {
            using var connection = _connectionFactory.Create();

            var taskData = connection.QuerySingle<Models.Task>(@"
                INSERT INTO Tasks (Text, Completed, EndDate, CategoryId) 
                OUTPUT INSERTED.*
                VALUES (@Text, 'false', @EndDate, @CategoryId)",
                task
            );

            return taskData;
        }

        public int DeleteTask(int id)
        {
            using var connection = _connectionFactory.Create();

            connection.Execute(
                "DELETE FROM Tasks WHERE Id = @Id", 
                new { Id = id }
            );

            return id;
        }

        public int CompleteTask(int id, bool completed)
        {
            using var connection = _connectionFactory.Create();

            connection.Execute(@"
                UPDATE Tasks SET Completed = @Completed
                WHERE Id = @Id", 
                new {
                    Id = id, 
                    Completed = !completed
                }
            );

            return id;
        }

        public IEnumerable<Category> GetCategories()
        {
            using var connection = _connectionFactory.Create();

            var categories = connection.Query<Category>("SELECT * FROM Categories");

            return categories;
        }

        public async Task<IDictionary<int, Category>> GetCategoriesById(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.Create();

            var categories = await connection.QueryAsync<Category>(
                "SELECT * FROM Categories WHERE Id IN @Ids",
                new { Ids = ids }
            );

            return categories.ToDictionary(x => x.Id);
        }

        public Category GetCategory(int id)
        {
            using var connection = _connectionFactory.Create();

            var category = connection.QueryFirst<Category>(
                "SELECT * FROM Categories WHERE Id = @Id",
                new { Id = id }
            );

            return category;
        }
    }
}
