using EmployeeDirectory.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Application.Interfaces
{
    public interface IEmployeeDirectoryDbContext
    {
        DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}