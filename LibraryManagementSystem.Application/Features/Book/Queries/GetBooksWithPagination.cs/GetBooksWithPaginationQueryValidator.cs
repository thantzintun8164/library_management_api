using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBooksWithPagination
{
    public class GetBooksWithPaginationQueryValidator : AbstractValidator<GetBooksWithPaginationQuery>
    {
        public GetBooksWithPaginationQueryValidator() : base() { }

    }
}

