using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private IBookRepository? _books;
        private IAuthorRepository? _authors;
        private ICategoryRepository? _categories;
        private IRefreshTokenRepository? _refreshTokens;
        private IBorrowRecordRepository? _borrowRecord;

        public IBookRepository Books => _books ??= new BookRepository(_context);
        public IAuthorRepository Authors => _authors ??= new AuthorRepository(_context);
        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
        public IRefreshTokenRepository RefreshTokens => _refreshTokens ??= new RefreshTokenRepository(_context);
        public IBorrowRecordRepository BorrowRecords => _borrowRecord ??= new BorrowRecordRepository(_context);

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
