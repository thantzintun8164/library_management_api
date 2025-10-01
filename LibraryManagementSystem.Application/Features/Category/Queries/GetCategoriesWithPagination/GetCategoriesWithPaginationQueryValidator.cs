using FluentValidation;
using LibraryManagementSystem.Application.Features.Category.Queries.GetCategoriesWithPagination;

internal class GetCategoriesWithPaginationQueryValidator : AbstractValidator<GetCategoriesWithPaginationQuery>
{
    public GetCategoriesWithPaginationQueryValidator() : base()
    {
    }
}