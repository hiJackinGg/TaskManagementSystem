using MediatR;
using TaskManagementSystem.Model.Models;
using TaskStatus = TaskManagementSystem.Model.Models.TaskStatus;

namespace TaskManagementSystem.Application.UseCases.Tasks.UpdateTaskStatus
{
    public class UpdateTaskStatusRequest : IRequest<TaskModel>
    {
        public long TaskId { get; set; }
        public TaskStatus Status { get; set; }
    }
}
