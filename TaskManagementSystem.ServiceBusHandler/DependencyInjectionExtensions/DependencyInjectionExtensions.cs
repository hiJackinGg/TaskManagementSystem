using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.ServiceBusHandler.DependencyInjectionExtensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTaskManagementSystemBus(this IServiceCollection services, string host, string username, string password)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ServiceBusHandler>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, "/", h => {
                        h.Username(username);
                        h.Password(password);
                    });
                    cfg.ConfigureEndpoints(context);

                    // if error occurs, it will retry to send 3 times with 1s delay
                    // if the error still occurs, the failed message will be sent to "_error" queue and stored there
                    // (as we just update the state of task status, no sense to redeliver the failed message again as it would have an outdated state
                    // so store it just for monitoring purposes)
                    cfg.UseMessageRetry(r =>
                    {
                        r.Interval(3, 1_000);
                    });
                });
            });

            services.AddScoped<IServiceBusPublisher, ServiceBusHandler>();
            services.AddScoped<IServiceBusReceiver, ServiceBusHandler>();

            return services;
        }
    }
}
