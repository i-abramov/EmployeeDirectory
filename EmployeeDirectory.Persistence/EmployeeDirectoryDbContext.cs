using EmployeeDirectory.Application.Interfaces;
using EmployeeDirectory.Domain;
using EmployeeDirectory.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Persistence
{
    public class EmployeeDirectoryDbContext
        : DbContext, IEmployeeDirectoryDbContext, IEmployeeBulkInserter
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDirectoryDbContext(DbContextOptions<EmployeeDirectoryDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeConfiguration());
            base.OnModelCreating(builder);
        }

        public async Task BulkInsertAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken)
        {
            await EFCore.BulkExtensions.DbContextBulkExtensions
                .BulkInsertAsync(this, employees.ToList(), cancellationToken: cancellationToken);
        }
    }

}