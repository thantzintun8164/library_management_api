using LibraryManagementSystem.Application.Features.Author.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorByName
{
    public record GetAuthorByNameQuery(string AuthorName) : IRequest<AuthorDto>;
}
