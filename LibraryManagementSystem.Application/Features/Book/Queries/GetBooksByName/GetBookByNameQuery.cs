using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByName
{
    public record GetBookByNameQuery(string BookName) : IRequest<List<BookDto>>;
}
