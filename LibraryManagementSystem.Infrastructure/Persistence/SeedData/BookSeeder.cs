using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Infrastructure.Persistence.SeedData
{
    internal class BookSeeder : IEntitySeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book { Name = "Harry Potter and the Philosopher's Stone", AuthorId = 1, CategoryId = 1, BookFileUrl = "harrypotter1.pdf", ContentType = "application/pdf", Description = "First book of the Harry Potter series.", NumberOfAvailableBook = 5, PublicationYear = 1997 },
                    new Book { Name = "A Game of Thrones", AuthorId = 2, CategoryId = 1, BookFileUrl = "got1.pdf", ContentType = "application/pdf", Description = "First book of the A Song of Ice and Fire series.", NumberOfAvailableBook = 3, PublicationYear = 1996 },
                    new Book { Name = "The Hobbit", AuthorId = 3, CategoryId = 1, BookFileUrl = "hobbit.pdf", ContentType = "application/pdf", Description = "Fantasy adventure novel by J.R.R. Tolkien.", NumberOfAvailableBook = 4, PublicationYear = 1937 },
                    new Book { Name = "Murder on the Orient Express", AuthorId = 4, CategoryId = 3, BookFileUrl = "orientexpress.pdf", ContentType = "application/pdf", Description = "A famous mystery novel by Agatha Christie.", NumberOfAvailableBook = 2, PublicationYear = 1934 },
                    new Book { Name = "Foundation", AuthorId = 5, CategoryId = 2, BookFileUrl = "foundation.pdf", ContentType = "application/pdf", Description = "Sci-Fi novel by Isaac Asimov.", NumberOfAvailableBook = 3, PublicationYear = 1951 }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
