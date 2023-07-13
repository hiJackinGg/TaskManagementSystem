using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Model.Models;

namespace TaskManagementSystem.Application.UseCases.Tasks.GetTasks
{
    public class GetTasksRequest : IRequest<IEnumerable<TaskModel>>
    {
    }
}
