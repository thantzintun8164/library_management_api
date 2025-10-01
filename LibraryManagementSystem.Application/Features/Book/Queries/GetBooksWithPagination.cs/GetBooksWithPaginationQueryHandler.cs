using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksWithPagination
{
    internal class GetBooksWithPaginationQueryHandler
        : IRequestHandler<GetBooksWithPaginationQuery, PaginatedResult<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBooksWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<BookDto>> Handle(GetBooksWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var booksQuery = _unitOfWork.Books.GetWithDetails().AsNoTracking();
            var totalCount = await booksQuery.CountAsync(cancellationToken);

            if (totalCount == 0)
                throw new NotFoundException("No books found");

            var booksDto = await booksQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return PaginatedResult<BookDto>.Create(booksDto, totalCount, request.PageNumber, request.PageSize, request.BaseUrl);
        }
    }
}

