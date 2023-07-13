using MediatR;
using System.Reflection;
using TaskManagementSystem.Application.UseCases.Tasks.CreateTask;
using TaskManagementSystem.DAL.DependencyInjectionExtensions;
using TaskManagementSystem.ServiceBusHandler.DependencyInjectionExtensions;
using TaskManagementSystem.Utils;

namespace TaskManagementSystem
{
    public class Program
    {
        private static readonly Assembly ApplicationAssembly = typeof(CreateTaskHandler).Assembly;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = GetConfiguration(builder);
            var connectionString = configuration.GetConnectionString("TaskManagementSystemConnectionString");
            var busSettings = configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(ApplicationAssembly);
            builder.Services.AddDal(connectionString);
            builder.Services.AddTaskManagementSystemBus(busSettings.Host, busSettings.Username, busSettings.Password);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static IConfiguration GetConfiguration(WebApplicationBuilder builder)
        {
            ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            return serviceProvider.GetService<IConfiguration>();
        }
    }
}