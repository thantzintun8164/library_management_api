using LibraryManagementSystem.API.Common.Responses;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Common.Results;
using LibraryManagementSystem.Application.Features.Book.Commands.AddBook;
using LibraryManagementSystem.Application.Features.Book.Commands.RemoveBook;
using LibraryManagementSystem.Application.Features.Book.Commands.UpdateAvailableBook;
using LibraryManagementSystem.Application.Features.Book.Commands.UpdateBook;
using LibraryManagementSystem.Application.Features.Book.Commands.UpdateBookFile;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Book.Queries.DownloadBookFile;
using LibraryManagementSystem.Application.Features.Book.Queries.GetAllAvailableBooks;
using LibraryManagementSystem.Application.Features.Book.Queries.GetAllBooks;
using LibraryManagementSystem.Application.Features.Book.Queries.GetBookById;
using LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByName;
using LibraryManagementSystem.Application.Features.Book.Queries.GetBooksWithPagination;
using LibraryManagementSystem.Application.Features.BorrowRecord.Commands.BorrowBook;
using LibraryManagementSystem.Application.Features.BorrowRecord.Commands.ReturnBook;
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
    [SwaggerTag("Manage books: add, update, delete, borrow, return, list, download, and pagination.")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string AdminRole = nameof(Role.Admin);
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get book by ID", Description = "Retrieve a specific book by its ID.")]
        [ProducesResponseType<BookDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(int id)
        {
            var bookDto = await _mediator.Send(new GetBookByIdQuery(id));
            return Ok(bookDto);
        }




        [HttpGet("search")]
        [SwaggerOperation(Summary = "Search for book by name", Description = "Retrieve all books by that match name.")]
        [ProducesResponseType<List<BookDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByName([FromQuery] string bookName)
        {
            var bookDtos = await _mediator.Send(new GetBookByNameQuery(bookName));
            return Ok(bookDtos);
        }




        [HttpGet]
        [ProducesResponseType<List<BookDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get all books", Description = "Retrieve all books.")]
        public async Task<IActionResult> GetAll()
        {
            var bookDtos = await _mediator.Send(new GetAllBooksQuery());
            return Ok(bookDtos);
        }


        [HttpGet("available")]
        [ProducesResponseType<List<BookDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get all available books", Description = "Retrieve all available books.")]
        public async Task<IActionResult> GetAllAvailableBooks()
        {
            var bookDtos = await _mediator.Send(new GetAllAvailableBooksQuery());
            return Ok(bookDtos);
        }




        [HttpGet("{id}/download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Download book file", Description = "Download the file of a specific book by its ID.")]
        public async Task<IActionResult> DownloadBook(int id)
        {
            var fileDto = await _mediator.Send(new DownloadBookFileQuery(id));
            return File(fileDto.FileBytes, fileDto.ContentType, fileDto.DownloadName);
        }




        [HttpGet("pagination")]
        [ProducesResponseType<PaginatedResult<BookDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get books with pagination", Description = "Retrieve books using pagination parameters 'pageNumber' and 'pageSize'.")]
        public async Task<IActionResult> GetWithPagination([FromQuery] int pageNumber, [FromQuery] int PageSize)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var paginatedResult = await _mediator.Send(new GetBooksWithPaginationQuery(pageNumber, PageSize, baseUrl));
            return Ok(paginatedResult);
        }



        [HttpPost]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Add a new book", Description = "Add a new book and return the created book ID.(Admin Only)")]
        public async Task<IActionResult> Add([FromForm] AddBookCommand command)
        {
            var bookId = await _mediator.Send(command);
            return Ok(Result.Success($"Book Added Successfully with id:{bookId}"));
        }




        [HttpPost("{id}/borrow")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Borrow a book", Description = "Borrow a book for the authenticated user with a specified due date.")]
        public async Task<IActionResult> BorrowBook(int id, BorrowBookCommand command)
        {
            if (id != command.BookId)
                throw new BadRequestException("Book ID mismatch.");

            int borrowId = await _mediator.Send(command);
            return Ok(Result.Success($"Book Borrowed Successfully with Borrowed Id:{borrowId}"));
        }




        [HttpPost("{id}/return")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Return a borrowed book", Description = "Return a previously borrowed book by the authenticated user.")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            await _mediator.Send(new ReturnBookCommand(id));
            return Ok(Result.Success("Book Returned Successfully"));
        }





        [HttpPut("{id}")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Update a book", Description = "Update the details of an existing book.(Admin Only)")]
        public async Task<IActionResult> Update(int id, UpdateBookCommand command)
        {
            if (id != command.BookId)
                throw new BadRequestException("Book ID mismatch.");

            await _mediator.Send(command);
            return Ok(Result.Success("Book Updated Successfully"));
        }




        [Authorize(Roles = AdminRole)]
        [HttpPatch("{id}/availability")]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Update book availability", Description = "Update the availability status of a book.(Admin Only)")]
        public async Task<IActionResult> UpdateAvailableBook(int id, UpdateAvailableBookCommand command)
        {
            if (id != command.BookId)
                throw new BadRequestException("Book ID mismatch.");

            await _mediator.Send(command);
            return Ok(Result.Success("Book Availability Updated Successfully"));
        }


        [HttpPatch("{id}/file")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Update book file", Description = "Update the file associated with a book.(Admin Only)")]
        public async Task<IActionResult> UpdateBookFile(int id, [FromForm] UpdateBookFileCommand command)
        {
            if (id != command.BookId)
                throw new BadRequestException("Book ID mismatch.");

            await _mediator.Send(command);
            return Ok(Result.Success("Book File Updated Successfully"));
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Delete a book", Description = "Delete a book by its ID.(Admin Only)")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new RemoveBookCommand(id));
            return Ok(Result.Success("Book Deleted Successfully"));
        }
    }
}
