using TaskManagementSystem.DAL.Models;
using TaskStatus = TaskManagementSystem.DAL.Models.TaskStatus;

namespace TaskManagementSystem.DAL.Repositories
{
    public interface ITaskRepository
    {
        public Task<long> AddNewTask(TaskData model);
        public Task<IEnumerable<TaskData>> GetAllTasks();
        public Task<TaskData> UpdateTaskStatus(TaskStatus newStatus, long taskId);
    }
}
