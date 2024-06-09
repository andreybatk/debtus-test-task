using DebtusTestTask.DB.Interfaces;
using DebtusTestTask.DB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DebtusTestTask.DB.DiContainer
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IWorkShiftRepository, WorkShiftRepository>();
        }
    }
}