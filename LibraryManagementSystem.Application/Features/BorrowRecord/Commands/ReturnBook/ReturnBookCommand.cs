using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.ReturnBook
{
    public record ReturnBookCommand(int BookId) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.BorrowedBook.AllOverDue,
            CachingKeys.BorrowedBook.AllReturned,
            CachingKeys.BorrowedBook.ByUserId,
        ];
    }
}
