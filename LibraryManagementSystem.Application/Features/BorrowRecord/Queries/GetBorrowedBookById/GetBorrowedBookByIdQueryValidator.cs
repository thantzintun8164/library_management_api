using FluentValidation;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBookById
{
    public class GetBorrowedBookByIdQueryValidator : AbstractValidator<GetBorrowedBookByIdQuery>
    {
        public GetBorrowedBookByIdQueryValidator()
        {
            RuleFor(x => x.BorrowId)
                   .NotEmpty().WithMessage("Borrow Id is required")
                   .GreaterThan(0).WithMessage("Borrow Id Must be Greater than 0");
        }
    }
}
