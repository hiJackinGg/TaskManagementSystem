using FluentAssertions;
using Moq;
using TaskManagementSystem.Application.UseCases.Tasks.GetTasks;
using TaskManagementSystem.DAL.Models;
using TaskManagementSystem.DAL.Repositories;
using TaskStatus = TaskManagementSystem.DAL.Models.TaskStatus;

namespace TaskManagementSystem.Application.Unit.Tests.Tasks.GetTasks
{
    public class GetTasksHandlerTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly GetTasksHandler _getTasksHandler;

        public GetTasksHandlerTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _getTasksHandler = new GetTasksHandler(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task GetTasksHandler_WhenTasksExist_ShouldReturnAllStoredTasks()
        {
            var expectedTasks = new List<TaskData>
            {
                new TaskData
                {
                    TaskId= 1,
                    Description = "testDescription1",
                    TaskName= "taskName1",
                    Status = TaskStatus.NotStarted,
                    AssignedTo = "testAssignedTo1"
                },
                new TaskData
                {
                    TaskId= 2,
                    Description = "testDescription2",
                    TaskName= "taskName2",
                    Status = TaskStatus.Completed,
                    AssignedTo = "testAssignedTo2"
                }
            };
            _taskRepositoryMock.Setup(r => r.GetAllTasks())
                .ReturnsAsync(expectedTasks)
                .Verifiable();

            var res = await _getTasksHandler.Handle(new GetTasksRequest(), default);

            res.Should().BeEquivalentTo(expectedTasks);

            _taskRepositoryMock.Verify(r => r.GetAllTasks(), Times.Once);
        }

        [Fact]
        public async Task GetTasksHandler_WhenThereAreNoTasks_ShouldReturnEmptyList()
        {
            _taskRepositoryMock.Setup(r => r.GetAllTasks())
                .ReturnsAsync(new List<TaskData>())
                .Verifiable();

            var res = await _getTasksHandler.Handle(new GetTasksRequest(), default);

            res.Should().BeEmpty();

            _taskRepositoryMock.Verify(r => r.GetAllTasks(), Times.Once);
        }
    }
}
