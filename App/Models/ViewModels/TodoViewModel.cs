using App.DTO;

namespace App.Models.ViewModels
{
    public class TodoViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<GetTasksDTO> Tasks { get; set; }
        public string Storage { get; set; }
    }
}
