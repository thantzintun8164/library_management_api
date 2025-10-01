using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBookById
{
    internal class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var bookQuery = _unitOfWork.Books.GetWithDetails().Where(x => x.Id == request.BookId).AsNoTracking();
            var bookDto = await bookQuery.ProjectTo<BookDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
            return bookDto ?? throw new NotFoundException($"No book found with Id: {request.BookId}");
        }
    }
}
