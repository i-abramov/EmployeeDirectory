using EmployeeDirectory.Application;
using EmployeeDirectory.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmployeeDirectory.Presentation
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var services = new ServiceCollection()
                .AddApplication()
                .AddPersistence(connectionString)
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
                .AddTransient<ConsoleApp>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}