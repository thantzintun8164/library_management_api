using FluentValidation;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.BorrowBook
{
    public class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
    {
        public BorrowBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Book Id must be greater than 0.");

            RuleFor(x => x.DueDate)
                .Must(BeAValidDueDate)
                .WithMessage("DueDate must be a future date and not more than 30 days from now.");
        }

        private bool BeAValidDueDate(DateTime dueDate)
        {
            var now = DateTime.UtcNow;
            return dueDate > now && dueDate <= now.AddDays(30);
        }
    }
}
