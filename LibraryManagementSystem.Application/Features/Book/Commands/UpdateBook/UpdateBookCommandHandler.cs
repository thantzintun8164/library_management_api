using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateBook
{
    internal class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.FindAsync(request.BookId);
            if (book == null)
                throw new NotFoundException($"Book with Id '{request.BookId}' not found.");

            //update 
            if (request.PublicationYear.HasValue)
                book.PublicationYear = request.PublicationYear.Value;

            if (!string.IsNullOrWhiteSpace(request.Name))
                book.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Description))
                book.Description = request.Description;

            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
