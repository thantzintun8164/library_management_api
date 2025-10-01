using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetAllReturnedBorrowBooks
{
    internal class GetAllReturnedBooksQueryHandler : IRequestHandler<GetAllReturnedBooksQuery, List<BorrowRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllReturnedBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<BorrowRecordDto>> Handle(GetAllReturnedBooksQuery request, CancellationToken cancellationToken)
        {
            var returnedQuery = _unitOfWork.BorrowRecords.GetAll(x => x.ReturnedDate != null).AsNoTracking();
            var borrowedRecordsDto = await returnedQuery.ProjectTo<BorrowRecordDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            return borrowedRecordsDto.Any() ? borrowedRecordsDto : throw new NotFoundException("No returned books found.");
        }
    }
}
