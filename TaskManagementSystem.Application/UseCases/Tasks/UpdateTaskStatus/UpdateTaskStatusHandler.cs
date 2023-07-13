using MediatR;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Model.Models;

namespace TaskManagementSystem.Application.UseCases.Tasks.UpdateTaskStatus
{
    public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusRequest, TaskModel>
    {
        private ITaskRepository _taskRepository;

        public UpdateTaskStatusHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskModel> Handle(UpdateTaskStatusRequest request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.UpdateTaskStatus((DAL.Models.TaskStatus)(int)request.Status, request.TaskId);

            if (task is null)
            {
                return null;
            }

            return new TaskModel
            {
                TaskId = task.TaskId,
                Description = task.Description,
                TaskName = task.TaskName,
                AssignedTo = task.AssignedTo,
                Status = task.Status.Map(),
            };
        }
    }
}
