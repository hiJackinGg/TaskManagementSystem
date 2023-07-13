using MassTransit;
using MediatR;
using Moq;
using TaskManagementSystem.Application.UseCases.Tasks.UpdateTaskStatus;
using TaskManagementSystem.ServiceBusHandler.Events;
using TaskStatus = TaskManagementSystem.Model.Models.TaskStatus;

namespace TaskManagementSystem.ServiceBusHandler.Unit.Tests.ServiceBus
{
    public class ServiceBusHandlerTests
    {
        private readonly Mock<IPublishEndpoint> _publishEndpointMock;
        private readonly Mock<IMediator> _mediatorMock;

        private readonly ServiceBusHandler _serviceBusHandler;

        public ServiceBusHandlerTests()
        {
            _publishEndpointMock = new Mock<IPublishEndpoint>();
            _mediatorMock = new Mock<IMediator>();
            _serviceBusHandler = new ServiceBusHandler(_publishEndpointMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task ServiceBusHandler_SendMessage_ShouldPublishMessage()
        {
            var msg = new TaskUpdateEvent
            {
                TaskId = 11,
                Status = TaskStatus.Completed,
                UpdatedBy = "testUser"
            };

            await _serviceBusHandler.SendMessage(msg);

            _publishEndpointMock.Verify(r => r.Publish(It.Is<TaskUpdateEvent>(t =>
                    t.TaskId == msg.TaskId &&
                    t.Status == msg.Status &&
                    t.UpdatedBy == msg.UpdatedBy
                ), default), Times.Once);
        }

        [Fact]
        public async Task ServiceBusHandler_ReceiveMessage_ShouldCallMediatorToUpdateTask()
        {
            var msg = new TaskUpdateEvent
            {
                TaskId = 11,
                Status = TaskStatus.Completed,
                UpdatedBy = "testUser"
            };

            await _serviceBusHandler.ReceiveMessage(msg);

            _mediatorMock.Verify(r => r.Send(It.Is<UpdateTaskStatusRequest>(t =>
                    t.TaskId == msg.TaskId &&
                    t.Status == msg.Status
                ), default), Times.Once);
        }
    }
}
