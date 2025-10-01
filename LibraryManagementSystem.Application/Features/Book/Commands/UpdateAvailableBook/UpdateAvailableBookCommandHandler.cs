using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateAvailableBook
{
    internal class UpdateAvailableBookCommandHandler : IRequestHandler<UpdateAvailableBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAvailableBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateAvailableBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.FindAsync(request.BookId);
            if (book == null)
                throw new NotFoundException($"Book with Id '{request.BookId}' not found.");

            book.NumberOfAvailableBook = request.NumberOfAvailableBook;
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
