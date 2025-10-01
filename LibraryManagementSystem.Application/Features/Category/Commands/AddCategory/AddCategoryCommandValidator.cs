using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Category.Commands.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name can't exceed 200 characters.")
                .MinimumLength(2).WithMessage("Name Must be greater than 2 character");


            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Name can't exceed 200 characters.")
                .MinimumLength(2).WithMessage("Name Must be greater than 2 character")
                .When(x => x.Description != null);

        }
    }
}
