using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetAllOverDueBorrowBooks
{
    internal class GetAllOverDueBooksQueryHandler : IRequestHandler<GetAllOverDueBooksQuery, List<BorrowRecordDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOverDueBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BorrowRecordDto>> Handle(GetAllOverDueBooksQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var overDueQuery = _unitOfWork.BorrowRecords.GetAll(x => x.DueDate < now && x.ReturnedDate == null).AsNoTracking();
            var borrowedRecordsDto = await overDueQuery.ProjectTo<BorrowRecordDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            return borrowedRecordsDto.Any() ? borrowedRecordsDto : throw new NotFoundException("No overdue borrowed books found.");
        }
    }
}


