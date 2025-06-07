using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Models;

namespace To_Do_List.Database
{
    public class ToDoDbContext : IdentityDbContext<User>
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDoTask>(x => 
            {
                x.HasKey(x => x.TaskId);

                x.HasOne(x => x.User)
                    .WithMany(y => y.ToDoTasks)
                    .HasForeignKey(x => x.UserId);
            });

        }
        
    }
}
