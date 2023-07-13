using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.DAL.Contexts;
using TaskManagementSystem.DAL.Models;
using TaskStatus = TaskManagementSystem.DAL.Models.TaskStatus;

namespace TaskManagementSystem.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _dbContext;

        public TaskRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> AddNewTask(TaskData model)
        {
            _dbContext.Tasks.Add(model);

            await _dbContext.SaveChangesAsync();

            return model.TaskId;
        }

        public async Task<IEnumerable<TaskData>> GetAllTasks()
        {
            return await _dbContext.Tasks
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskData> UpdateTaskStatus(TaskStatus newStatus, long taskId)
        {
            var task = new TaskData() { TaskId = taskId };

            _dbContext.Tasks.Attach(task);
            task.Status = newStatus;
            _dbContext.Entry(task).Property(x => x.Status).IsModified = true;

            await _dbContext.SaveChangesAsync();

            return task;
        }
    }
}
