using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<ToDoTask>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasMany(u => u.Tasks).WithOne(t => t.User);
        }
    }

}
