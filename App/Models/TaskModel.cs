namespace App.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime? EndDate { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
