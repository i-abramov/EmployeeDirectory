using EmployeeDirectory.Presentation;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        Startup.Configure();

        using var scope = Startup.ServiceProvider.CreateScope();
        var app = scope.ServiceProvider.GetRequiredService<ConsoleApp>();

        await app.RunAsync(args);
    }
}