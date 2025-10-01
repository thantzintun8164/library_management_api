using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Book.Commands.RemoveBook
{
    public class RemoveBookCommandValidator : AbstractValidator<RemoveBookCommand>
    {
        public RemoveBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Book Id is required")
                .GreaterThan(0).WithMessage("Book Id can't be negative");
        }
    }
}
