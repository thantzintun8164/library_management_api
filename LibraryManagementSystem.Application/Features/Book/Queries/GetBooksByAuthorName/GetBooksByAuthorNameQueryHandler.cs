using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByAuthorName
{
    internal class GetBooksByAuthorNameQueryHandler : IRequestHandler<GetBooksByAuthorNameQuery, List<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBooksByAuthorNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(GetBooksByAuthorNameQuery request, CancellationToken cancellationToken)
        {
            var booksQuery = _unitOfWork.Books.GetAll(x => x.Author.Name == request.AuthorName).AsNoTracking();
            var booksDto = await booksQuery.ProjectTo<BookDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return booksDto.Any() ? booksDto : throw new NotFoundException($"no books found with author name:'{request.AuthorName}'");
        }
    }
}
