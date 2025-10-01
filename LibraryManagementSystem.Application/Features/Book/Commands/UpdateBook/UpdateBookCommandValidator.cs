using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                   .NotEmpty().WithMessage("Book Id is required")
                   .GreaterThan(0).WithMessage("Book Id can't be negative");

            RuleFor(x => x.Name)
                   .MaximumLength(100).WithMessage("Book Name cannot exceed 100 characters.")
                   .When(x => x.Name != null);

            RuleFor(x => x.Description)
                   .MaximumLength(4000).WithMessage("Book Description cannot exceed 4000 characters.")
                   .When(x => x.Description != null);

            RuleFor(x => x.PublicationYear)
                   .InclusiveBetween(1500, DateTime.UtcNow.Year)
                   .WithMessage($"Publication year must be between 1500 and {DateTime.UtcNow.Year}.")
                   .When(x => x.PublicationYear != null);

        }
    }
}
