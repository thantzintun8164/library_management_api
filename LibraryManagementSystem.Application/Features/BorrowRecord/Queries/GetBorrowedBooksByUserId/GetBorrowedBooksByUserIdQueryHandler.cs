using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBooksByUserId
{
    internal class GetBorrowedBooksByUserIdQueryHandler : IRequestHandler<GetBorrowedBooksByUserIdQuery, List<BorrowRecordDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBorrowedBooksByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BorrowRecordDto>> Handle(GetBorrowedBooksByUserIdQuery request, CancellationToken cancellationToken)
        {
            var borrowedRecordsQuery = _unitOfWork.BorrowRecords.GetAll(x => x.UserId == request.UserId).AsNoTracking();
            var borrowedRecordsDto = await borrowedRecordsQuery.ProjectTo<BorrowRecordDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);


            return borrowedRecordsDto.Any()
               ? borrowedRecordsDto :
                throw new NotFoundException($"No borrowed books found for user with ID '{request.UserId}'.");
        }
    }
}