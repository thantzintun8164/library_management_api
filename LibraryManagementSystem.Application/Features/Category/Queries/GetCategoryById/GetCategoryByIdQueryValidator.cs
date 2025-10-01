using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Category.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category Id is required")
                .GreaterThan(0).WithMessage("Category Id can't be negative");
        }
    }
}
