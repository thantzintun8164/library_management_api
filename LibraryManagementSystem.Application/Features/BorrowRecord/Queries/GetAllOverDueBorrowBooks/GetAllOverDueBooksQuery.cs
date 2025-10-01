using LibraryManagementSystem.Application.Common.Caching;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetAllOverDueBorrowBooks
{
    public record GetAllOverDueBooksQuery : ICachableRequest<List<BorrowRecordDto>>
    {
        public string CacheKey => CachingKeys.BorrowedBook.AllOverDue;
    }
}
