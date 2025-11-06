using EmployeeDirectory.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EmployeeDirectoryDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IEmployeeDirectoryDbContext>(provider =>
                provider.GetRequiredService<EmployeeDirectoryDbContext>());

            services.AddScoped<IEmployeeBulkInserter>(provider =>
                provider.GetRequiredService<EmployeeDirectoryDbContext>());

            return services;
        }
    }
}