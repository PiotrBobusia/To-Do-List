using Microsoft.EntityFrameworkCore;
using To_Do_List.Database;
using To_Do_List.Models;

namespace To_Do_List.Repository
{
    public class ToDoTasksRepository : IToDoTasksRepository
    {
        private ToDoDbContext _context;
        public ToDoTasksRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public async Task AddTaskAsync(ToDoTask task)
        {
            await _context.ToDoTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoTask>> GetUserTasksAsync(string userId)
        {
            return await _context.ToDoTasks.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<ToDoTask>> GetUnfinishedUserTasksAsync(string userId)
        {
            return await _context.ToDoTasks.Where(x => x.UserId == userId && x.Done == false).ToListAsync();
        }

        public async Task SetDoneAsync(int taskId)
        {
            var task = _context.ToDoTasks.FirstOrDefault(x => x.TaskId == taskId);

            if (task is null) return;

            task.Done = true;

            _context.ToDoTasks.Update(task);
            await _context.SaveChangesAsync();
        }

    }
}
