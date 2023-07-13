using MediatR;
using TaskStatus = TaskManagementSystem.Model.Models.TaskStatus;

namespace TaskManagementSystem.Application.UseCases.Tasks.CreateTask
{
    public class CreateTaskRequest : IRequest<long>
    {
        public string TaskName { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public string? AssignedTo { get; set; }
    }
}
