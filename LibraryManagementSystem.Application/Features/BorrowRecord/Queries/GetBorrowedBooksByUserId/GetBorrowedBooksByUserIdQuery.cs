using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBooksByUserId
{
    public record GetBorrowedBooksByUserIdQuery(string UserId) : ICachableRequest<List<BorrowRecordDto>>
    {
        public string CacheKey => CachingKeys.BorrowedBook.ByUserId;
    }
}
