using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category Id is required")
                .GreaterThan(0).WithMessage("Category Id can't be negative");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name can't exceed 200 characters.")
                .MinimumLength(2).WithMessage("Name Must be greater than 2 character")
                .When(x => x.Name != null);

            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Name can't exceed 200 characters.")
                .MinimumLength(2).WithMessage("Name Must be greater than 2 character")
                .When(x => x.Description != null);
        }
    }
}
