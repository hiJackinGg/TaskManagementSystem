using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskStatus = TaskManagementSystem.DAL.Models.TaskStatus;

namespace TaskManagementSystem.DAL.Contexts
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<TaskData> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskData>()
                .HasKey(x => x.TaskId);
            modelBuilder.Entity<TaskData>()
                .Property(x => x.AssignedTo)
                .IsRequired(false);
            modelBuilder
                .Entity<TaskData>()
                .Property(d => d.Status)
                .HasConversion(new EnumToStringConverter<TaskStatus>());
        }
    }
}
