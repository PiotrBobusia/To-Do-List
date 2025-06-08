using To_Do_List.Models;

namespace To_Do_List.Repository
{
    public interface IToDoTasksRepository
    {
        Task<bool> AddTaskAsync(ToDoTask task);
        Task<IEnumerable<ToDoTask>> GetUserTasksAsync(string userId);
    }
}