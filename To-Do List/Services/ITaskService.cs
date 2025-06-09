using System.Security.Claims;
using To_Do_List.Models.DTOs;

namespace To_Do_List.Services
{
    public interface ITaskService
    {
        Task AddTaskByUser(ClaimsPrincipal user, ToDoTaskDTO taskDTO);
        Task<IEnumerable<ToDoTaskDTO>> GetTasksListByUser(ClaimsPrincipal user);
        Task SetDoneByTaskId(int id);
    }
}