using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.RenewBorrowedBook
{
    internal class RenewBorrowedBookCommandHandler : IRequestHandler<RenewBorrowedBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RenewBorrowedBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RenewBorrowedBookCommand request, CancellationToken cancellationToken)
        {
            var borrowRecord = await _unitOfWork.BorrowRecords.FindAsync(request.BorrowId);
            if (borrowRecord == null)
                throw new NotFoundException($"Borrow Record with Id '{request.BorrowId}' not found.");

            borrowRecord.DueDate = request.NewDueData;
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
