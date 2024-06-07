using App.DTO;
using App.Models;
using App.Models.ViewModels;
using App.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Controllers
{
	public class HomeController : Controller
	{
        private readonly IRepositoryController _repositoryController;
		private readonly IRepository _repository;

        public HomeController(IRepositoryController repositoryController)
        {
            _repositoryController = repositoryController;
            _repository = repositoryController.Load();
        }

		public IActionResult Index()
		{
            IEnumerable<Category> categories = _repository.GetCategories();
			IEnumerable<GetTasksDTO> tasks = _repository.GetTasksWithCategory();

			var data = new TodoViewModel
            {
                Categories = categories,
				Tasks = tasks,
                Storage = _repositoryController.Storage
            };

			return View(data);
		}

		[HttpPost]
        public IActionResult Index(CreateTaskDTO task)
		{
            _repository.CreateTask(task);

			return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeStorage(string storage)
        {
            _repositoryController.ChangeRepository(storage);

            return RedirectToAction("Index");
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
