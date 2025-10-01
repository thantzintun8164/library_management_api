using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Account.Queries.GetUsersWithPagination
{
    public class GetUsersWithPaginationQueryValidator : AbstractValidator<GetUsersWithPaginationQuery>
    {
        public GetUsersWithPaginationQueryValidator() : base() { }
    }
}
