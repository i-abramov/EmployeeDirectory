using EmployeeDirectory.Application.Common.Helpers;
using EmployeeDirectory.Application.Employees.Commands.AddEmployee;
using EmployeeDirectory.Application.Employees.Commands.AddEmployeeListBulk;
using EmployeeDirectory.Application.Employees.Queries.GetEmployeeListByFilters;
using EmployeeDirectory.Application.Employees.Queries.GetUniqueEmployeeList;
using EmployeeDirectory.Domain.Enums;
using EmployeeDirectory.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.Presentation
{
    public class ConsoleApp
    {
        private readonly IMediator _mediator;

        public ConsoleApp(IMediator mediator) => _mediator = mediator;

        public async Task RunAsync(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }

            var mode = args[0].ToLowerInvariant();

            switch (mode)
            {
                case "1":
                    await CreateTableAsync();
                    break;

                case "2":
                    await AddEmployeeAsync(args);
                    break;

                case "3":
                    await ShowUniqueEmployeesAsync();
                    break;

                case "4":
                    await GenerateEmployees();
                    break;

                case "5":
                    await FilterEmployeesAsync(EmployeeGender.Male, 'F');
                    break;

                default:
                    Console.WriteLine("Unknown mode.");
                    break;
            }

            Console.ReadKey();
        }

        private async Task CreateTableAsync()
        {
            using var scope = Startup.ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EmployeeDirectoryDbContext>();

            await DbInitializer.InitializeAsync(context);

            Console.WriteLine("Table \"Employees\" created successfully.");
        }


        private async Task AddEmployeeAsync(string[] args)
        {
            if (args.Length < 4)
            {
                return;
            }

            await _mediator.Send(new AddEmployeeCommand
            {
                FullName = args[1],
                DateOfBirth = DateOnly.Parse(args[2]),
                Gender = Enum.Parse<EmployeeGender>(args[3], ignoreCase: true)
            });
            Console.WriteLine($"Employee {args[1]} added.");
        }

        private async Task ShowUniqueEmployeesAsync()
        {
            var vm = await _mediator.Send(new GetUniqueEmployeeListQuery());

            EmployeePrinter.Print(vm.Employees);
        }

        private async Task GenerateEmployees()
        {
            var count = 1000000;

            var employees = EmployeeGenerator.Generate(count);
            
            await _mediator.Send(new AddEmployeeListBulkCommand
            {
                Employees = employees
            });

            Console.WriteLine($"{count} employees generated and added.");

            count = 100;
            var maleF = EmployeeGenerator.GenerateWithParameters(count, EmployeeGender.Male, 'F');

            await _mediator.Send(new AddEmployeeListBulkCommand
            {
                Employees = maleF
            });

            Console.WriteLine($"{count} employees generated and added.");
        }

        private async Task FilterEmployeesAsync(EmployeeGender gender, char lastNameStartsWith)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var vm = await _mediator.Send(new GetEmployeeListByFiltersQuery
            {
                Gender = gender,
                LastNameStartsWith = lastNameStartsWith
            });

            stopwatch.Stop();

            Console.WriteLine($"Found {vm.Employees.Count} employees in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}