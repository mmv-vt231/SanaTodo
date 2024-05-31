namespace App.DTO
{
    public class GetTasksDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
