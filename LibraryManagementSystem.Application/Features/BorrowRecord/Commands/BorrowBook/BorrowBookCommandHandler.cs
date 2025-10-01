using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Identity;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;
using BorrowRecordEntity = LibraryManagementSystem.Domain.Entities.BorrowRecord;
namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.BorrowBook
{
    internal class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public BorrowBookCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnAuthorizedException("User not authorized");

            var book = await _unitOfWork.Books.FindAsync(request.BookId);
            if (book == null)
                throw new NotFoundException($"Book with Id '{request.BookId}' not found.");

            if (book.NumberOfAvailableBook <= 0)
                throw new NotFoundException("No available copies of the book to borrow.");

            book.NumberOfAvailableBook -= 1;
            var borrowBook = new BorrowRecordEntity
            {
                BookId = request.BookId,
                UserId = _currentUserService.UserId!,
                DueDate = request.DueDate,
            };
            await _unitOfWork.BorrowRecords.AddAsync(borrowBook);
            await _unitOfWork.SaveAsync();
            return borrowBook.Id;
        }
    }
}
