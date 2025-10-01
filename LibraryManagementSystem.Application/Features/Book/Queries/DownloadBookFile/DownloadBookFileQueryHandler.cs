using LibraryManagementSystem.Application.Common.DTOs;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Contracts.Services;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Queries.DownloadBookFile
{
    internal class DownloadBookFileQueryHandler : IRequestHandler<DownloadBookFileQuery, DownLoadFileDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        public DownloadBookFileQueryHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<DownLoadFileDto> Handle(DownloadBookFileQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.FindAsync(request.BookId);
            if (book == null)
                throw new NotFoundException("book not Found");

            var fileByteResult = await _fileStorageService.DownloadFileAsync(book.BookFileUrl);
            if (!fileByteResult.IsSuccess)
                throw new NotFoundException(fileByteResult.Message);
            
            return new DownLoadFileDto
            {
                FileBytes = fileByteResult.Data!,
                DownloadName = book.Name,
                ContentType = book.ContentType,
            };

        }
    }
}
