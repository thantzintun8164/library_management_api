using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Contracts.Services;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateBookFile
{
    internal class UpdateBookFileCommandHandler : IRequestHandler<UpdateBookFileCommand, Unit>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookFileCommandHandler(IFileStorageService fileStorageService, IUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateBookFileCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.FindAsync(request.BookId);
            if (book == null)
                throw new NotFoundException($"Book with Id '{request.BookId}' not found.");

            var mediaResult = await _fileStorageService.UploadFileAsync(request.NewFile);
            if (!mediaResult.IsSuccess)
                throw new BadRequestException(mediaResult.Message);

            _fileStorageService.Remove(book.BookFileUrl);

            book.BookFileUrl = mediaResult.Data!.FileUrl;
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
