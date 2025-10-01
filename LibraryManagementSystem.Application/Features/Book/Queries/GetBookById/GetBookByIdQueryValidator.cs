using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Book.Queries.GetBookById
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(x => x.BookId)
                  .NotEmpty().WithMessage("Book Id is required")
                  .GreaterThan(0).WithMessage("Book Id Must be Greater than 0");
        }
    }
}
