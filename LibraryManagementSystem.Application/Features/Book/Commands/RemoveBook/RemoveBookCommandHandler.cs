using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Contracts.Services;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands.RemoveBook
{
    internal class RemoveBookCommandHandler : IRequestHandler<RemoveBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        public RemoveBookCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }
        public async Task<Unit> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.FindAsync(request.BookId);
            if (book == null)
                throw new NotFoundException($"Book with Id '{request.BookId}' not found.");
            
            _fileStorageService.Remove(book.BookFileUrl);
            _unitOfWork.Books.Remove(book);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
