using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBooksWithPagination
{
    internal class GetBorrowedBooksWithPaginationQueryHandler
        : IRequestHandler<GetBorrowedBooksWithPaginationQuery, PaginatedResult<BorrowRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBorrowedBooksWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<BorrowRecordDto>> Handle(GetBorrowedBooksWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var borrowBooksQuery = _unitOfWork.BorrowRecords.GetWithDetails().AsNoTracking();
            var totalCount = await borrowBooksQuery.CountAsync();
            if (totalCount == 0)
                throw new NotFoundException("No borrow books found");

            var borrowBooksDto = await borrowBooksQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<BorrowRecordDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return PaginatedResult<BorrowRecordDto>.Create(borrowBooksDto, totalCount, request.PageNumber, request.PageSize, request.BaseUrl);
        }
    }
}
