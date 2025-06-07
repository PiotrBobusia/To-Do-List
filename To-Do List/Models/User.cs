using Microsoft.AspNetCore.Identity;

namespace To_Do_List.Models
{
    public class User : IdentityUser
    {
        public List<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();
    }
}
