using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.UseCases.Tasks.UpdateTaskStatus;
using TaskManagementSystem.ServiceBusHandler;
using TaskManagementSystem.ServiceBusHandler.Events;

namespace TaskManagementSystem.UseCases.Tasks.UpdateTaskStatus
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskManagementSystemController : ControllerBase
    {
        public readonly IServiceBusPublisher _serviceBus;

        public TaskManagementSystemController(IServiceBusPublisher serviceBus)
        {
            _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskStatus(UpdateTaskStatusRequest request)
        {
            await _serviceBus.SendMessage(new TaskUpdateEvent
            {
                TaskId = request.TaskId,
                Status = request.Status,
                UpdatedBy = AppDomain.CurrentDomain.FriendlyName // as we don't have authentication, just use app name
            });

            return Ok();
        }
    }
}
