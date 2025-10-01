using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByName
{
    internal class GetBookByNameQueryHandler : IRequestHandler<GetBookByNameQuery, List<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBookByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<BookDto>> Handle(GetBookByNameQuery request, CancellationToken cancellationToken)
        {

            var bookQuery = _unitOfWork.Books.GetWithDetails().Where(x => x.Name.Contains(request.BookName)).AsNoTracking();
            var booksDto = await bookQuery.ProjectTo<BookDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return booksDto.Any() ? booksDto : throw new NotFoundException($"No books found with name: {request.BookName}");
        }
    }
}
