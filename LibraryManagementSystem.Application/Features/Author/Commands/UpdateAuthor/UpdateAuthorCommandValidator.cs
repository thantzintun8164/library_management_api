using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Author.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is Required")
             .NotNull().WithMessage("Name can't be null")
             .MaximumLength(100).WithMessage("Name Must be less than 100 character")
             .MinimumLength(2).WithMessage("Name}Must be greater than 2 character");

            RuleFor(x => x.AuthorId)
               .NotNull().WithMessage("Id Can't be null")
               .GreaterThan(0).WithMessage("ID Can't be negative");
        }
    }
}
