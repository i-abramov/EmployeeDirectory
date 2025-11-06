using EmployeeDirectory.Domain;
using MediatR;

namespace EmployeeDirectory.Application.Employees.Commands.AddEmployeeListBulk
{
    public class AddEmployeeListBulkCommand : IRequest<Unit>
    {
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
    }
}