
namespace TaskManagementSystem.Application.Utils
{
    public static class TaskStatusExtensions
    {
        public static Model.Models.TaskStatus Map(this DAL.Models.TaskStatus status) =>
            (Model.Models.TaskStatus)(int)status;

        public static DAL.Models.TaskStatus Map(this Model.Models.TaskStatus status) =>
            (DAL.Models.TaskStatus)(int)status;
    }
}
