using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetAllBorrowedBooks
{
    public record GetAllBorrowedBooksQuery : IRequest<List<BorrowRecordDto>>;
}
