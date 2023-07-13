using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.ServiceBusHandler.Events;

namespace TaskManagementSystem.ServiceBusHandler
{
    public interface IServiceBusPublisher
    {
        public Task SendMessage(TaskUpdateEvent eventMsg);
    }
}
