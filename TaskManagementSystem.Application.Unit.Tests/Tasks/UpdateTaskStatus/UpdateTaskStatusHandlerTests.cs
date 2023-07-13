using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.UseCases.Tasks.CreateTask;
using TaskManagementSystem.Application.UseCases.Tasks.UpdateTaskStatus;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.DAL.Models;
using TaskManagementSystem.DAL.Repositories;

namespace TaskManagementSystem.Application.Unit.Tests.Tasks.UpdateTaskStatus
{
    public class UpdateTaskStatusHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly UpdateTaskStatusHandler _updateTaskStatusHandler;

        public UpdateTaskStatusHandlerTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _updateTaskStatusHandler = new UpdateTaskStatusHandler(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task UpdateTaskStatusHandler_WhenRequestIsValid_ShouldUpdateTaskStatus()
        {
            var updatedTask = new TaskData
            {
                TaskId = 11,
                TaskName = "Test",
                Description = "Description",
                AssignedTo = "testUser",
                Status = DAL.Models.TaskStatus.Completed
            };

            _taskRepositoryMock.Setup(r => r.UpdateTaskStatus(It.IsAny<DAL.Models.TaskStatus>(), It.IsAny<long>()))
                .ReturnsAsync(updatedTask)
                .Verifiable();

            var res = await _updateTaskStatusHandler.Handle(new UpdateTaskStatusRequest
            {
                TaskId = updatedTask.TaskId,
                Status = (Model.Models.TaskStatus)(int)updatedTask.Status
            }, default);

            res.Should().BeEquivalentTo(updatedTask);

            _taskRepositoryMock.Verify(r => r.UpdateTaskStatus(updatedTask.Status, updatedTask.TaskId), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskStatusHandler_WhenRequestIsValidButUpdateTaskStatusReturnsNull_ShouldReturnNull()
        {
            var taskId = 11;
            var status = Model.Models.TaskStatus.Completed;

            _taskRepositoryMock.Setup(r => r.UpdateTaskStatus(It.IsAny<DAL.Models.TaskStatus>(), It.IsAny<long>()))
                .ReturnsAsync(() => null)
                .Verifiable();

            var res = await _updateTaskStatusHandler.Handle(new UpdateTaskStatusRequest
            {
                TaskId = taskId,
                Status = status
            }, default);

            res.Should().BeNull();

            _taskRepositoryMock.Verify(r => r.UpdateTaskStatus(status.Map(), taskId), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskStatusHandler_WhenError_ShouldThrowException()
        {
            var taskId = 11;
            var status = Model.Models.TaskStatus.Completed;

            var expectedException = new Exception("Error");

            _taskRepositoryMock.Setup(r => r.UpdateTaskStatus(It.IsAny<DAL.Models.TaskStatus>(), It.IsAny<long>()))
                .ThrowsAsync(expectedException)
                .Verifiable();

            Func<Task> action = async () => await _updateTaskStatusHandler.Handle(new UpdateTaskStatusRequest
            {
                TaskId = taskId,
                Status = status
            }, default);

            var ex = await action.Should().ThrowAsync<Exception>();
            ex.Which.Message.Should().BeEquivalentTo("Error");
        }
    }
}
