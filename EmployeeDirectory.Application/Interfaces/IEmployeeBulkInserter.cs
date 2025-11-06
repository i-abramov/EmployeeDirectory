using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Application.Interfaces
{
    public interface IEmployeeBulkInserter
    {
        Task BulkInsertAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken);
    }

}