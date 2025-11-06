using FluentValidation;

namespace EmployeeDirectory.Application.Employees.Queries.GetEmployeeListByFilters
{
    public class GetEmployeeListByFiltersQueryValidator : AbstractValidator<GetEmployeeListByFiltersQuery>
    {
        public GetEmployeeListByFiltersQueryValidator()
        {
            RuleFor(employeeList => employeeList.LastNameStartsWith).Must(s => char.IsLetter(s));
        }
    }
}