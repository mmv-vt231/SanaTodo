using App.DTO;
using App.Models;
using App.XMLStorage;
using System.Xml.Linq;

namespace App.Repository
{
    public class XMLRepository : IXMLRepository
    {
        private readonly IXMLFactory _connectionFactory;

        public XMLRepository(IXMLFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<GetTasksDTO> GetTasksWithCategory()
        {
            XElement storage = _connectionFactory.Load().Element("storage");

            var tasks = storage.Element("tasks")
                .Descendants("task")
                .Select(t =>
                {
                    var category = storage.Element("categories")
                        .Descendants("category")
                        .FirstOrDefault(c => (int)c.Element("id") == (int)t.Element("categoryId"))
                        .Element("name").Value;

                    DateTime? endData = !string.IsNullOrEmpty(t.Element("endDate").Value) && t.Element("endDate").Value != "null"
                        ? DateTime.Parse(t.Element("endDate").Value)
                        : null;

                    return new GetTasksDTO
                    {
                        Id = (int)t.Element("id"),
                        Text = (string)t.Element("text"),
                        Completed = (bool)t.Element("completed"),
                        EndDate = endData,
                        Category = category
                    };
                })
                .ToList();

            return tasks;
        }

        public IEnumerable<Models.Task> GetTasks()
        {
            XElement storage = _connectionFactory.Load().Element("storage");

            var tasks = storage.Element("tasks")
                .Descendants("task")
                .Select(t =>
                {
                    DateTime? endData = !string.IsNullOrEmpty(t.Element("endDate").Value) && t.Element("endDate").Value != "null"
                        ? DateTime.Parse(t.Element("endDate").Value)
                        : null;

                    return new Models.Task
                    {
                        Id = (int)t.Element("id"),
                        Text = (string)t.Element("text"),
                        Completed = (bool)t.Element("completed"),
                        EndDate = endData,
                        CategoryId = (int)t.Element("categoryId"),
                    };
                })
                .ToList();

            return tasks;
        }

        public Models.Task CreateTask(CreateTaskDTO task)
        {
            XDocument storage = _connectionFactory.Load();

            var lastTask = storage.Element("storage")
                ?.Element("tasks")
                ?.Descendants("task")
                .LastOrDefault();

            var id = lastTask != null ? (int)lastTask.Element("id") + 1 : 1;

            var newTask = new XElement("task",
                new XElement("id", id),
                new XElement("text", task.Text),
                new XElement("completed", false),
                new XElement("endDate", task.EndDate),
                new XElement("categoryId", task.CategoryId)
            );

            storage.Element("storage")
                ?.Element("tasks")
                ?.Add(newTask);

            _connectionFactory.Save(storage);

            return new Models.Task { 
                Id = id,
                Text = task.Text,
                Completed = false,
                EndDate = task.EndDate,
                CategoryId = task.CategoryId,
            };
        }

        public int CompleteTask(int id, bool completed)
        {
            XDocument storage = _connectionFactory.Load();

            storage.Element("storage")
                .Element("tasks")
                .Descendants("task")
                .FirstOrDefault(t => (int)t.Element("id") == id)
                .Element("completed").Value = (!completed).ToString().ToLower();

            _connectionFactory.Save(storage);

            return id;
        }

        public int DeleteTask(int id)
        {
            XDocument storage = _connectionFactory.Load();

            storage.Element("storage")
                .Element("tasks")
                .Descendants("task")
                .Where(t => (int)t.Element("id") == id)
                .Remove();

            _connectionFactory.Save(storage);

            return id;
        }

        public IEnumerable<Category> GetCategories()
        {
            XDocument storage = _connectionFactory.Load();

            var categories = storage.Element("storage")
                .Element("categories")
                .Descendants("category")
                .Select(c => new Category
                {
                    Id = (int)c.Element("id"),
                    Name = (string)c.Element("name")
                }).ToList();

            return categories;
        }

        public Category GetCategory(int id)
        {
            XDocument storage = _connectionFactory.Load();

            var category = storage.Element("storage")
                .Element("categories")
                .Descendants("category")
                .FirstOrDefault(c => (int)c.Element("id") == id);

            return new Category
            {
                Id = (int)category.Element("id"),
                Name = (string)category.Element("name")
            };
        }

        public async Task<IDictionary<int, Category>> GetCategoriesById(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            XDocument storage = _connectionFactory.Load();

            var categories = storage.Element("storage")
                .Element("categories")
                .Descendants("category")
                .Where(c => ids.Contains((int)c.Element("id")))
                .Select(c => new Category
                {
                    Id = (int)c.Element("id"),
                    Name = (string)c.Element("name")
                }).ToList();

            return categories.ToDictionary(x => x.Id);
        }
    }
}
