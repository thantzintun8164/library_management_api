using LibraryManagementSystem.API.Common.Responses;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Common.Results;
using LibraryManagementSystem.Application.Features.BorrowRecord.Commands.RenewBorrowedBook;
using LibraryManagementSystem.Application.Features.BorrowRecord.DTOs;
using LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetAllBorrowedBooks;
using LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetAllOverDueBorrowBooks;
using LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetAllReturnedBorrowBooks;
using LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBookById;
using LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBooksByUserId;
using LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBooksWithPagination;
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
    [SwaggerTag("Manage borrow records: view, renew, list all or overdue or returned, and pagination.")]
    public class BorrowBooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string AdminRole = nameof(Role.Admin);
        public BorrowBooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<BorrowRecordDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get Borrow Record by ID", Description = "Retrieve a specific borrow record by its unique identifier. (Admin only)")]
        public async Task<IActionResult> GetBorrowedBookById(int id)
        {
            var borrowRecordDto = await _mediator.Send(new GetBorrowedBookByIdQuery(id));
            return Ok(borrowRecordDto);
        }



        [HttpGet]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<List<BorrowRecordDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get All Borrow Records", Description = "Retrieve all current borrow records. (Admin only)")]
        public async Task<IActionResult> GetAllBorrowedBooks()
        {
            var borrowRecordDtos = await _mediator.Send(new GetAllBorrowedBooksQuery());
            return Ok(borrowRecordDtos);
        }



        [HttpGet("overdue")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<List<BorrowRecordDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get Overdue Borrow Records", Description = "Retrieve all borrow records where the due date has passed. (Admin only)")]
        public async Task<IActionResult> GetAllOverdueBooks()
        {
            var borrowRecordDtos = await _mediator.Send(new GetAllOverDueBooksQuery());
            return Ok(borrowRecordDtos);
        }



        [HttpGet("returned")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<List<BorrowRecordDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get Returned Borrow Records", Description = "Retrieve all borrow records that have been marked as returned. (Admin only)")]
        public async Task<IActionResult> GetAllReturnedBorrowBooks()
        {
            var borrowRecordDtos = await _mediator.Send(new GetAllReturnedBooksQuery());
            return Ok(borrowRecordDtos);
        }



        [HttpGet("user/{userId}")]
        [ProducesResponseType<List<BorrowRecordDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get Borrow Records by User", Description = "Retrieve all borrow records for a specific user.")]
        public async Task<IActionResult> GetBorrowedBooksByUserId(string userId)
        {
            var borrowRecordDtos = await _mediator.Send(new GetBorrowedBooksByUserIdQuery(userId));
            return Ok(borrowRecordDtos);
        }



        [HttpGet("pagination")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<PaginatedResult<BorrowRecordDto>>(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get Paginated Borrow Records", Description = "Retrieve borrow records with pagination using 'pageNumber' and 'pageSize'. (Admin only)")]
        public async Task<IActionResult> GetBorrowedBooksWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var paginatedResult = await _mediator.Send(new GetBorrowedBooksWithPaginationQuery(pageNumber, pageSize, baseUrl));
            return Ok(paginatedResult);
        }



        [HttpPatch("{id}/renew")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Renew borrowed book", Description = "Extend the due date of a borrowed book. BorrowId in body must match path ID.(Admin Only)")]
        public async Task<IActionResult> RenewBorrowedBook(int id, [FromBody] RenewBorrowedBookCommand command)
        {
            if (id != command.BorrowId)
                throw new BadRequestException("Borrow record ID mismatch.");

            await _mediator.Send(command);
            return Ok(Result.Success("Borrowed book renewed successfully."));
        }
    }
}
