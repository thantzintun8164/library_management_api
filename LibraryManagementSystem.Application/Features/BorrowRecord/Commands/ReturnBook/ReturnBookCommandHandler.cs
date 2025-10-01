using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.ReturnBook
{
    internal class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ReturnBookCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnAuthorizedException("User not authorized");
            var userId = _currentUserService.UserId!;

            var borrowedBook = await _unitOfWork.BorrowRecords.GetAsync(x => x.BookId == request.BookId && x.UserId == userId);
            if (borrowedBook == null)
                throw new NotFoundException($"Borrowed Book with BookId '{request.BookId}' & UserID '{userId}' not found.");

            var book = await _unitOfWork.Books.FindAsync(request.BookId);
            if (book == null)
                throw new NotFoundException($"Book with Id '{request.BookId}' not found.");

            book.NumberOfAvailableBook += 1;
            borrowedBook.ReturnedDate = DateTime.UtcNow;
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
