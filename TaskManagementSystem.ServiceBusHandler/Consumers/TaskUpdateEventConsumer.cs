using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.ServiceBusHandler.Events;

namespace TaskManagementSystem.ServiceBusHandler.Consumers
{
    public abstract class TaskUpdateEventConsumer : IConsumer<TaskUpdateEvent>, IServiceBusReceiver
    {
        public Task Consume(ConsumeContext<TaskUpdateEvent> context)
        {
            return ReceiveMessage(context.Message);
        }

        public abstract Task ReceiveMessage(TaskUpdateEvent eventMsg);
    }
}
