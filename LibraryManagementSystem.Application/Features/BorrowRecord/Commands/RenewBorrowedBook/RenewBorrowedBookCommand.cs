using LibraryManagementSystem.Application.Common.Caching;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.RenewBorrowedBook
{
    public record RenewBorrowedBookCommand(int BorrowId, DateTime NewDueData) : IRequest<Unit>, ICacheRemoval
    {
        public string[] CacheKeys =>
        [
            CachingKeys.BorrowedBook.AllOverDue,
            CachingKeys.BorrowedBook.ByUserId,
        ];
    }
}
