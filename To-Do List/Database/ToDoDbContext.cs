using Microsoft.EntityFrameworkCore;
using To_Do_List.Models;

namespace To_Do_List.Database
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDoTask> ToDoTasks { get; set; }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>(x => x.HasKey(x => x.TaskId));
        }
    }
}
