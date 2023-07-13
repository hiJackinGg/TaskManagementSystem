using TaskStatus = TaskManagementSystem.Model.Models.TaskStatus;

namespace TaskManagementSystem.ServiceBusHandler.Events
{
    public class TaskUpdateEvent
    {
        public long TaskId { get; set; }
        public TaskStatus Status { get; set; }
        public string UpdatedBy { get; set; }
    }
}
