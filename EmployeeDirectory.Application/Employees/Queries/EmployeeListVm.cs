using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Application.Employees.Queries
{
    public class EmployeeListVm
    {
        public IList<Employee> Employees { get; set; } = [];
    }
}