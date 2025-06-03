using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Models
{
    public class ToDoTask
    {
        public required int TaskId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateOnly MyProperty { get; set; }
        public Priority Priority { get; set; }
        public bool Done { get; set; } = false;
    }
}
