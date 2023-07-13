using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.ServiceBusHandler.Events;

namespace TaskManagementSystem.ServiceBusHandler
{
    public interface IServiceBusReceiver
    {
        public Task ReceiveMessage(TaskUpdateEvent eventMsg);
    }
}
