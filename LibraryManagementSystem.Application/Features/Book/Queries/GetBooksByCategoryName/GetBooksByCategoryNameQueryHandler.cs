using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByCategoryName
{
    internal class GetBooksByCategoryNameQueryHandler : IRequestHandler<GetBooksByCategoryNameQuery, List<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBooksByCategoryNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(GetBooksByCategoryNameQuery request, CancellationToken cancellationToken)
        {
            var booksQuery = _unitOfWork.Books.GetAll(x => x.Category.Name == request.CategoryName).AsNoTracking();
            var booksDto = await booksQuery.ProjectTo<BookDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return booksDto.Any() ? booksDto : throw new NotFoundException($"no books found with category name:'{request.CategoryName}'");
        }

    }
}
