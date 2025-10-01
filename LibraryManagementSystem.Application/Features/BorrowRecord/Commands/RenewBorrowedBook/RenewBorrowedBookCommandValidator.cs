using FluentValidation;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Commands.RenewBorrowedBook
{
    public class RenewBorrowedBookCommandValidator : AbstractValidator<RenewBorrowedBookCommand>
    {
        public RenewBorrowedBookCommandValidator()
        {
            RuleFor(x => x.BorrowId)
                .GreaterThan(0).WithMessage("Borrow Id must be greater than 0.");

            RuleFor(x => x.NewDueData)
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
