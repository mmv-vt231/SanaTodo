using App.DTO;

namespace App.Models
{
    public class TodoViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<GetTasksDTO> Tasks { get; set; }
    }
}
