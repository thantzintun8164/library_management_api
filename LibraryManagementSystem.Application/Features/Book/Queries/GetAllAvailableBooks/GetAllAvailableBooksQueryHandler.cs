using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Book.Queries.GetAllBooks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetAllAvailableBooks
{
    internal class GetAllAvailableBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAvailableBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var booksQuery = _unitOfWork.Books.GetAll(x => x.NumberOfAvailableBook > 0).AsNoTracking();
            var booksDto = await booksQuery.ProjectTo<BookDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return booksDto.Any() ? booksDto : throw new NotFoundException("No Books found");
        }
    }
}
