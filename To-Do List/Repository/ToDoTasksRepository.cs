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

        public async Task<bool> AddTaskAsync(ToDoTask task)
        {
            await _context.ToDoTasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ToDoTask>> GetUserTasksAsync(string userId)
        {
            return await _context.ToDoTasks.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
