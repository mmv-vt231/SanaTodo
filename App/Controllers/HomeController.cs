using App.DTO;
using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Controllers
{
	public class HomeController : Controller
	{
		private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

		public IActionResult Index()
		{
            IEnumerable<Category> categories = _repository.GetCategories();
			IEnumerable<GetTasksDTO> tasks = _repository.GetTasks();

			var data = new TodoViewModel
            {
                Categories = categories,
				Tasks = tasks,
            };

			return View(data);
		}

		[HttpPost]
        public IActionResult Index(CreateTaskDTO task)
		{
			if(ModelState.IsValid)
			{
                _repository.CreateTask(task);
				return RedirectToAction("Index");
            }

			return View();
		}

        [HttpPost("todo/complete")]
        public IActionResult CompleteTask(int id, bool completed)
        {
            _repository.CompleteTask(id, completed);

			return Ok();
        }

        [HttpDelete("todo/delete")]
        public IActionResult DeleteTask(int id)
        {
            _repository.DeleteTask(id);

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
