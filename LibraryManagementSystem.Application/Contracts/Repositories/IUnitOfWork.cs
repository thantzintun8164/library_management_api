using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryManagementSystem.Application.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        ICategoryRepository Categories { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IBorrowRecordRepository BorrowRecords { get; }
        Task<int> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}
