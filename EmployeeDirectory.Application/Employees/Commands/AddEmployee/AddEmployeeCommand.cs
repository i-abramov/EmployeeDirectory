using EmployeeDirectory.Domain.Enums;
using MediatR;

namespace EmployeeDirectory.Application.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommand : IRequest<Guid>
    {
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public EmployeeGender Gender { get; set; }
    }
}