using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateAvailableBook
{
    public class UpdateAvailableBookCommandValidator : AbstractValidator<UpdateAvailableBookCommand>
    {
        public UpdateAvailableBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Book Id is required")
                .GreaterThan(0).WithMessage("Book Id can't be negative");

            RuleFor(x => x.NumberOfAvailableBook)
                .NotEmpty().WithMessage("Available Book is required")
                .GreaterThan(0).WithMessage("Number of available book Id must be greather than 0");


        }
    }
}
