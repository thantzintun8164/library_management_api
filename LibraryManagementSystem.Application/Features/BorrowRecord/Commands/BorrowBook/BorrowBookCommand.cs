using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.BorrowBook
{
    public record BorrowBookCommand(int BookId, DateTime DueDate) : IRequest<int>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.BorrowedBook.AllOverDue,
            CachingKeys.BorrowedBook.ByUserId,
        ];
    }
}
