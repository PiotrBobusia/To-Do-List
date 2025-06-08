namespace To_Do_List.Models.DTOs
{
    public class ToDoTaskDTO
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateOnly Date { get; set; } = default;
        public Priority Priority { get; set; }
        public bool Done { get; set; } = false;
    }
}
