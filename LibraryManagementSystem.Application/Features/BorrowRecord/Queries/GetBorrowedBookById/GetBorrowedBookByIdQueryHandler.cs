using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBookById
{
    internal class GetBorrowedBookByIdQueryHandler : IRequestHandler<GetBorrowedBookByIdQuery, BorrowRecordDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBorrowedBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BorrowRecordDto> Handle(GetBorrowedBookByIdQuery request, CancellationToken cancellationToken)
        {
            var borrowedRecordQuery = _unitOfWork.BorrowRecords.GetWithDetails().Where(x => x.Id == request.BorrowId).AsNoTracking();
            var borrowedRecordDto = await borrowedRecordQuery.ProjectTo<BorrowRecordDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return borrowedRecordDto ?? throw new NotFoundException($"No borrowed book found with ID '{request.BorrowId}'.");
        }
    }
}
