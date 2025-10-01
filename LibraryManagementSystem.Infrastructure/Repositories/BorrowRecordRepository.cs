using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Context;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BorrowRecordRepository : GenericRepository<BorrowRecord>, IBorrowRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowRecordRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
