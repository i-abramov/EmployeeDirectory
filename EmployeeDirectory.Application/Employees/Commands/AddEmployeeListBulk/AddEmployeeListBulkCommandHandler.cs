using EmployeeDirectory.Application.Interfaces;
using MediatR;

namespace EmployeeDirectory.Application.Employees.Commands.AddEmployeeListBulk
{
    public class AddEmployeeListBulkCommandHandler
        : IRequestHandler<AddEmployeeListBulkCommand, Unit>
    {
        private readonly IEmployeeBulkInserter _bulkInserter;

        public AddEmployeeListBulkCommandHandler(IEmployeeBulkInserter bulkInserter) =>
            _bulkInserter = bulkInserter;

        public async Task<Unit> Handle(AddEmployeeListBulkCommand request, CancellationToken cancellationToken)
        {
            await _bulkInserter.BulkInsertAsync(request.Employees, cancellationToken);
            return Unit.Value;
        }
    }
}