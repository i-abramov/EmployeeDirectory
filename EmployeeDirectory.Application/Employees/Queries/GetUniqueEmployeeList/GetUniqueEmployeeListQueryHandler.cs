using EmployeeDirectory.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Application.Employees.Queries.GetUniqueEmployeeList
{
    public class GetUniqueEmployeeListQueryHandler : IRequestHandler<GetUniqueEmployeeListQuery, EmployeeListVm>
    {
        private readonly IEmployeeDirectoryDbContext _dbContext;

        public GetUniqueEmployeeListQueryHandler(IEmployeeDirectoryDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<EmployeeListVm> Handle(GetUniqueEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var uniqueIds = await _dbContext.Employees
                .AsNoTracking()
                .GroupBy(e => new { e.FullName, e.DateOfBirth })
                .Select(g => g.Min(e => e.ID))
                .ToListAsync(cancellationToken);

            var uniqueEmployees = await _dbContext.Employees
                .AsNoTracking()
                .Where(e => uniqueIds.Contains(e.ID))
                .OrderBy(e => e.FullName)
                .ToListAsync(cancellationToken);

            return new EmployeeListVm
            {
                Employees = uniqueEmployees
            };
        }

    }
}