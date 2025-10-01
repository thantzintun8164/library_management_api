using AutoMapper;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Contracts.Services;
using MediatR;
using BookEntity = LibraryManagementSystem.Domain.Entities.Book;
namespace LibraryManagementSystem.Application.Features.Book.Commands.AddBook
{
    internal class AddBookCommandHandler : IRequestHandler<AddBookCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var categoryExist = await _unitOfWork.Categories.AnyAsync(x => x.Id == request.CategoryId);
            if (!categoryExist)
                throw new NotFoundException($"Category with Id '{request.CategoryId}' not found.");
            var authorExist = await _unitOfWork.Authors.AnyAsync(x => x.Id == request.AuthorId);
            if (!authorExist)
                throw new NotFoundException($"Author with Id '{request.CategoryId}' not found.");

            var uploadResult = await _fileStorageService.UploadFileAsync(request.BookFile);
            if (!uploadResult.IsSuccess)
                throw new BadRequestException(uploadResult.Message);

            var book = _mapper.Map<BookEntity>(request);
            book.BookFileUrl = uploadResult.Data!.FileUrl;
            book.ContentType = uploadResult.Data!.ContentType;

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveAsync();
            return book.Id;
        }
    }
}
