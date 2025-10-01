using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBookById
{
    public record GetBorrowedBookByIdQuery(int BorrowId) : IRequest<BorrowRecordDto>;
}
