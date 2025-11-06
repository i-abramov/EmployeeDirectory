using EmployeeDirectory.Application.Interfaces;
using EmployeeDirectory.Domain;
using MediatR;

namespace EmployeeDirectory.Application.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, Guid>
    {
        private readonly IEmployeeDirectoryDbContext _dbContext;

        public AddEmployeeCommandHandler(IEmployeeDirectoryDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(AddEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                ID = Guid.NewGuid(),
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender
            };

            await _dbContext.Employees.AddAsync(employee, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return employee.ID;
        }
    }
}