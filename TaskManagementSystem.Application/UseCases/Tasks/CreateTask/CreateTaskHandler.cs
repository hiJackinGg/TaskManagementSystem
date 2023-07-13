using MediatR;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.DAL.Models;
using TaskManagementSystem.DAL.Repositories;

namespace TaskManagementSystem.Application.UseCases.Tasks.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskRequest, long>
    {
        private ITaskRepository _taskRepository;

        public CreateTaskHandler(ITaskRepository taskRepository) 
        {
            _taskRepository = taskRepository;
        }

        public Task<long> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            return _taskRepository.AddNewTask(new TaskData
            {
                TaskName = request.TaskName,
                Description = request.Description,
                Status = request.Status.Map(),
                AssignedTo = request.AssignedTo
            });
        }
    }
}
