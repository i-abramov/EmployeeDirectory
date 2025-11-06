using EmployeeDirectory.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Application.Employees.Queries.GetEmployeeListByFilters
{
    public class GetEmployeeListByFiltersQueryHandler : IRequestHandler<GetEmployeeListByFiltersQuery, EmployeeListVm>
    {
        private readonly IEmployeeDirectoryDbContext _dbContext;

        public GetEmployeeListByFiltersQueryHandler(IEmployeeDirectoryDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<EmployeeListVm> Handle(GetEmployeeListByFiltersQuery request, CancellationToken cancellationToken)
        {
            var employees = await _dbContext.Employees
                .AsNoTracking()
                .Where(e => e.FullName != null &&
                            e.Gender == request.Gender &&
                            EF.Functions.Like(e.FullName!, $"{request.LastNameStartsWith}%"))
                .OrderBy(e => e.FullName)
                .ToListAsync(cancellationToken);


            return new EmployeeListVm { Employees = employees };
        }
    }
}