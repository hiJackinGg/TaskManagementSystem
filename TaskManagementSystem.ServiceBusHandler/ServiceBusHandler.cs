using MassTransit;
using MediatR;
using TaskManagementSystem.Application.UseCases.Tasks.UpdateTaskStatus;
using TaskManagementSystem.ServiceBusHandler.Consumers;
using TaskManagementSystem.ServiceBusHandler.Events;

namespace TaskManagementSystem.ServiceBusHandler
{
    public class ServiceBusHandler : TaskUpdateEventConsumer, IServiceBusPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;

        public ServiceBusHandler(IPublishEndpoint publishEndpoint, IMediator mediator)
        {
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        public override Task ReceiveMessage(TaskUpdateEvent eventMsg)
        {
            return _mediator.Send(new UpdateTaskStatusRequest
            {
                TaskId = eventMsg.TaskId,
                Status = eventMsg.Status
            });
        }

        public Task SendMessage(TaskUpdateEvent eventMsg)
        {
            return _publishEndpoint.Publish(eventMsg);
        }
    }
}