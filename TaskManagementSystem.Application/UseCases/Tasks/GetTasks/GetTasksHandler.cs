using MediatR;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.DAL.Repositories;
using TaskManagementSystem.Model.Models;

namespace TaskManagementSystem.Application.UseCases.Tasks.GetTasks
{
    public class GetTasksHandler : IRequestHandler<GetTasksRequest, IEnumerable<TaskModel>>
    {
        private ITaskRepository _taskRepository;

        public GetTasksHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskModel>> Handle(GetTasksRequest request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllTasks();

            if (!tasks.Any())
            {
                return Enumerable.Empty<TaskModel>();
            }

            return tasks.Select(t => new TaskModel
            {
                TaskId = t.TaskId,
                TaskName = t.TaskName,
                Status = t.Status.Map(),
                AssignedTo = t.AssignedTo,
                Description = t.Description
            });
        }
    }
}
