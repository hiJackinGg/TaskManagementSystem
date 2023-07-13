using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.DAL.Contexts;
using TaskManagementSystem.DAL.Repositories;

namespace TaskManagementSystem.DAL.DependencyInjectionExtensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDal(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TaskDbContext>(
                options => options.UseSqlServer(connectionString));

            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
