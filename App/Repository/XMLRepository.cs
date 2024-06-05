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

        public void CompleteTask(int id, bool completed)
        {
            XDocument storage = _connectionFactory.Load();

            storage.Element("storage")
                .Element("tasks")
                .Descendants("task")
                .FirstOrDefault(t => (int)t.Element("id") == id)
                .Element("completed").Value = (!completed).ToString().ToLower();

            _connectionFactory.Save(storage);
        }

        public void CreateTask(CreateTaskDTO task)
        {
            XDocument storage = _connectionFactory.Load();

            var lastTask = storage.Element("storage")
                ?.Element("tasks")
                ?.Descendants("task")
                .LastOrDefault();

            var id = lastTask != null ? (int)lastTask.Element("id") + 1 : 1;

            storage.Element("storage")
                ?.Element("tasks")
                ?.Add(new XElement("task", 
                    new XElement("id", id),
                    new XElement("text", task.Text),
                    new XElement("completed", false),
                    new XElement("endDate", task.EndDate),
                    new XElement("categoryId", task.CategoryId)
                ));

            _connectionFactory.Save(storage);
        }

        public void DeleteTask(int id)
        {
            XDocument storage = _connectionFactory.Load();

            storage.Element("storage")
                .Element("tasks")
                .Descendants("task")
                .Where(t => (int)t.Element("id") == id)
                .Remove();

            _connectionFactory.Save(storage);
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

        public IEnumerable<GetTasksDTO> GetTasks()
        {
            XElement storage = _connectionFactory.Load().Element("storage");

            var tasks = storage.Element("tasks")
                .Descendants("task")
                .Select(t => new GetTasksDTO
                {
                    Id = (int)t.Element("id"),
                    Text = (string)t.Element("text"),
                    Completed = (bool)t.Element("completed"),
                    EndDate = !string.IsNullOrEmpty(t.Element("endDate").Value) && t.Element("endDate").Value != "null"
                                ? DateTime.Parse(t.Element("endDate").Value)
                                : null,
                    Category = storage.Element("categories")
                                .Descendants("category")
                                .FirstOrDefault(c => (int)c.Element("id") == (int)t.Element("categoryId")).Element("name").Value
                })
                .ToList();

            return tasks;
        }
    }
}
