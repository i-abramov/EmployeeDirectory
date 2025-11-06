using EmployeeDirectory.Domain.Enums;
using MediatR;

namespace EmployeeDirectory.Application.Employees.Queries.GetEmployeeListByFilters
{
    public class GetEmployeeListByFiltersQuery : IRequest<EmployeeListVm>
    {
        public EmployeeGender Gender { get; set; }
        public char LastNameStartsWith { get; set; }
    }
}