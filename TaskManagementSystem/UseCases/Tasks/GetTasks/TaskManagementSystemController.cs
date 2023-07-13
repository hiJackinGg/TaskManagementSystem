using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.UseCases.Tasks.GetTasks;
using TaskManagementSystem.Model.Models;

namespace TaskManagementSystem.UseCases.Tasks.GetTasks
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskManagementSystemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskManagementSystemController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            return _mediator.Send(new GetTasksRequest());
        }
    }
}
