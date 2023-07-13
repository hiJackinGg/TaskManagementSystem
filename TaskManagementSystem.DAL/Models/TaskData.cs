using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.DAL.Models
{
    public class TaskData
    {
        public long TaskId {get;set;}
        public string TaskName { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public string AssignedTo { get; set; }
    }
}
