using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorsWithPagination
{
    public class GetAuthorsWithPaginationQueryValidator : AbstractValidator<GetAuthorsWithPaginationQuery>
    {
        public GetAuthorsWithPaginationQueryValidator() : base() { }

    }
}
