using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Author.Commands.RemoveAuthor
{
    public class RemoveAuthorCommandValidator : AbstractValidator<RemoveAuthorCommand>
    {
        public RemoveAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotNull().WithMessage("Id Can't be null")
                .GreaterThan(0).WithMessage("ID Can't be negative");
        }
    }
}
