using LibraryManagementSystem.API.Common.Responses;
using LibraryManagementSystem.Application.Common.Exceptions;
using LibraryManagementSystem.Application.Common.PaginatedResult;
using LibraryManagementSystem.Application.Common.Results;
using LibraryManagementSystem.Application.Features.Author.DTOs;
using LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorsByCategoryName;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Book.Queries.GetBooksByCategoryName;
using LibraryManagementSystem.Application.Features.Category.Commands.AddCategory;
using LibraryManagementSystem.Application.Features.Category.Commands.RemoveCategory;
using LibraryManagementSystem.Application.Features.Category.Commands.UpdateCategory;
using LibraryManagementSystem.Application.Features.Category.DTOs;
using LibraryManagementSystem.Application.Features.Category.Queries.GetAllCategories;
using LibraryManagementSystem.Application.Features.Category.Queries.GetCategoriesWithPagination;
using LibraryManagementSystem.Application.Features.Category.Queries.GetCategoryById;
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
    [SwaggerTag("Manage book categories: list, get by ID, pagination, add, update, delete, and related books/authors.")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string AdminRole = nameof(Role.Admin);

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("{id}")]
        [ProducesResponseType<CategoryDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get category by ID", Description = "Retrieve a specific category by its ID.")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoryDto = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(categoryDto);
        }



        [HttpGet]
        [ProducesResponseType<List<CategoryDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get all categories", Description = "Retrieve all book categories.")]
        public async Task<IActionResult> GetAll()
        {
            var categoryDtos = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categoryDtos);
        }



        [HttpGet("pagination")]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<PaginatedResult<CategoryDto>>(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get categories with pagination", Description = "Retrieve categories using pagination parameters 'pageNumber' and 'pageSize'.")]
        public async Task<IActionResult> GetWithPagination([FromQuery] int pageNumber, [FromQuery] int PageSize)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var paginatedResult = await _mediator.Send(new GetCategoriesWithPaginationQuery(pageNumber, PageSize, baseUrl));
            return Ok(paginatedResult);
        }



        [HttpGet("{categoryName:alpha}/books")]
        [ProducesResponseType<List<BookDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get books by category", Description = "Retrieve all books belonging to a specific category.")]
        public async Task<IActionResult> GetBooksByCategoryName(string categoryName)
        {
            var bookDtos = await _mediator.Send(new GetBooksByCategoryNameQuery(categoryName));
            return Ok(bookDtos);
        }



        [HttpGet("{categoryName:alpha}/authors")]
        [ProducesResponseType<List<AuthorDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Get authors by category", Description = "Retrieve all authors related to a specific category.")]
        public async Task<IActionResult> GetAuthorsByCategoryName(string categoryName)
        {
            var authorDtos = await _mediator.Send(new GetAuthorsByCategoryNameQuery(categoryName));
            return Ok(authorDtos);
        }



        [HttpPost]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Add a new category", Description = "Create a new book category.(Admin Only)")]
        public async Task<IActionResult> Add(AddCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            return Ok(Result.Success($"Category Added Successfully with id:{categoryId}"));
        }


        [HttpPut("{id}")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Update a category", Description = "Update an existing book category.(Admin Only)")]
        public async Task<IActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if (id != command.CategoryId)
                throw new BadRequestException("Category ID mismatch.");

            await _mediator.Send(command);
            return Ok(Result.Success("Category Updated Successfully."));
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = AdminRole)]
        [ProducesResponseType<Result>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Delete a category", Description = "Remove a book category by its ID.(Admin Only)")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new RemoveCategoryCommand(id));
            return Ok(Result.Success("Category Deleted Successfully."));
        }
    }
}
