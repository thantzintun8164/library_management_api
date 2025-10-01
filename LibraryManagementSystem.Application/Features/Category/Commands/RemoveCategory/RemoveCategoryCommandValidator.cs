using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Category.Commands.RemoveCategory
{
    public class RemoveCategoryCommandValidator : AbstractValidator<RemoveCategoryCommand>
    {
        public RemoveCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId)
             .NotEmpty().WithMessage("Category Id is required")
             .GreaterThan(0).WithMessage("Category Id can't be negative");
        }
    }
}
