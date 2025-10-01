using FluentValidation;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.ReturnBook
{
    public class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Book Id must be greater than 0.");
        }
    }

}
