using System.ComponentModel.DataAnnotations;

namespace App.DTO
{
    public class CreateTaskDTO
    {
        [Required]
        public string Text { get; set; } = string.Empty;

        public DateTime? EndDate { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
