using FluentAssertions;
using Moq;
using TaskManagementSystem.Application.UseCases.Tasks.CreateTask;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.DAL.Models;
using TaskManagementSystem.DAL.Repositories;
using TaskStatus = TaskManagementSystem.Model.Models.TaskStatus;

namespace TaskManagementSystem.Application.Unit.Tests.Tasks.CreateTask
{
    public class CreateTaskHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly CreateTaskHandler _createTaskHandler;

        public CreateTaskHandlerTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _createTaskHandler = new CreateTaskHandler(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateTaskHandler_WhenRequestIsValid_ShouldSaveToDatabase()
        {
            var req = new CreateTaskRequest
            {
                TaskName= "Test",
                Description = "Description",    
                AssignedTo = "testUser",
                Status = TaskStatus.NotStarted
            };

            long expectedTaskId = 11;

            _taskRepositoryMock.Setup(r => r.AddNewTask(It.IsAny<TaskData>()))
                .ReturnsAsync(expectedTaskId)
                .Verifiable();

            var res = await _createTaskHandler.Handle(req, default);

            res.Should().Be(expectedTaskId);

            _taskRepositoryMock.Verify(r => r.AddNewTask(It.Is<TaskData>(t => 
                    t.TaskId == 0 &&
                    t.TaskName == req.TaskName &&
                    t.Description == req.Description &&
                    t.Status == req.Status.Map() &&
                    t.AssignedTo == req.AssignedTo
                )), Times.Once);
        }

        [Fact]
        public async Task CreateTaskHandler_WhenError_ShouldThrowException()
        {
            var req = new CreateTaskRequest
            {
                TaskName = "Test",
                Description = "Description",
                AssignedTo = "testUser",
                Status = TaskStatus.NotStarted
            };

            var expectedException = new Exception("Error");

            _taskRepositoryMock.Setup(r => r.AddNewTask(It.IsAny<TaskData>()))
                .ThrowsAsync(expectedException)
                .Verifiable();

            Func<Task> action = async () => await _createTaskHandler.Handle(req, default);

            var ex = await action.Should().ThrowAsync<Exception>();
            ex.Which.Message.Should().BeEquivalentTo("Error");
        }
    }
}
