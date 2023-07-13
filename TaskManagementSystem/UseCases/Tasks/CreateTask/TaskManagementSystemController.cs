using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.UseCases.Tasks.CreateTask;

namespace TaskManagementSystem.UseCases.Tasks.CreateTask
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

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            var resultModel = await _mediator.Send(request);
            return Created(nameof(CreateTask), resultModel);
        }
    }
}
