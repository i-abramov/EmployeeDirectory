using FluentValidation;

namespace EmployeeDirectory.Application.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
    {
        public AddEmployeeCommandValidator()
        {
            
        }
    }
}