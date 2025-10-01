using LibraryManagementSystem.API.Common.Responses;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Common.Results;
using LibraryManagementSystem.Application.Features.Author.Commands.AddAuthor;
using LibraryManagementSystem.Application.Features.Author.Commands.RemoveAuthor;
using LibraryManagementSystem.Application.Features.Author.Commands.UpdateAuthor;
using LibraryManagementSystem.Application.Features.Author.DTOs;
using LibraryManagementSystem.Application.Features.Author.Queries.GetAllAuthors;
using LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorById;
using LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorsWithPagination;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByAuthorName;
using LibraryManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Manage authors: list, add, update, delete, and get books by author.")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string AdminRole = nameof(Role.Admin);
        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType<List<AuthorDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get all authors", Description = "Retrieve a list of all authors.")]
        public async Task<IActionResult> GetAll()
        {
            var authorDtos = await _mediator.Send(new GetAllAuthorsQuery());
            return Ok(authorDtos);
        }



        [HttpGet("{id}")]
        [ProducesResponseType<AuthorDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get author by ID", Description = "Retrieve a specific author by its ID.")]
        public async Task<IActionResult> GetById(int id)
        {
            var authorDto = await _mediator.Send(new GetAuthorByIdQuery(id));
            return Ok(authorDto);
        }




        [HttpGet("{authorName:alpha}/books")]
        [ProducesResponseType<List<BookDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get books by author name", Description = "Retrieve all books written by a specific author.")]
        public async Task<IActionResult> GetBooksByAuthorName(string authorName)
        {
            var bookDtos = await _mediator.Send(new GetBooksByAuthorNameQuery(authorName));
            return Ok(bookDtos);
        }


        [HttpGet("pagination")]
        [ProducesResponseType<PaginatedResult<AuthorDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get authors with pagination", Description = "Retrieve authors using pagination parameters 'pageNumber' and 'pageSize'.")]
        public async Task<IActionResult> GetWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var paginatedResult = await _mediator.Send(new GetAuthorsWithPaginationQuery(pageNumber, pageSize, baseUrl));
            return Ok(paginatedResult);
        }



        [HttpPost]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status201Created)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Add a new author", Description = "Create a new author and return its ID (Admin only).")]
        public async Task<IActionResult> Add(AddAuthorCommand command)
        {
            var authorId = await _mediator.Send(command);
            return Ok(Result.Success($"Author Added Successfully with id:{authorId}"));
        }


        [HttpPut("{id}")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Update an author", Description = "Update the details of an existing author. The ID in the path must match the ID in the body (Admin only).")]
        public async Task<IActionResult> Update(int id, UpdateAuthorCommand command)
        {
            if (id != command.AuthorId)
                throw new BadRequestException("Author ID mismatch.");

            await _mediator.Send(command);
            return Ok(Result.Success("Author Updated Successfully"));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Delete an author", Description = "Remove an author by its ID (Admin only).")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new RemoveAuthorCommand(id));
            return Ok(Result.Success("Author Deleted Successfully"));
        }
    }
}