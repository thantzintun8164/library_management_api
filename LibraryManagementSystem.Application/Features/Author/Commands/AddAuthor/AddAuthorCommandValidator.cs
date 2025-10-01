using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Author.Commands.AddAuthor
{
    public class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is Required")
                .NotNull().WithMessage("Name can't be null")
                .MaximumLength(100).WithMessage("Name Must be less than 100 character")
                .MinimumLength(2).WithMessage("Name}Must be greater than 2 character");
        }
    }
}
