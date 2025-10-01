using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetAllBorrowedBooks
{
    internal class GetAllBorrowedBooksQueryHandler : IRequestHandler<GetAllBorrowedBooksQuery, List<BorrowRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBorrowedBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BorrowRecordDto>> Handle(GetAllBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            var borrowedRecordsQuery = _unitOfWork.BorrowRecords.GetAll().AsNoTracking();
            var borrowedRecordsDto = await borrowedRecordsQuery.ProjectTo<BorrowRecordDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
            
            return borrowedRecordsDto.Any() ? borrowedRecordsDto : throw new NotFoundException("No Borrow Books found"); ;
        }
    }
}
