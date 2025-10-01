using FluentValidation;

namespace LibraryManagementSystem.Application.Features.BorrowRecord.Queries.GetBorrowedBooksByUserId
{
    public class GetBorrowedBooksByUserIdQueryValidator : AbstractValidator<GetBorrowedBooksByUserIdQuery>
    {
        public GetBorrowedBooksByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                   .NotEmpty().WithMessage("Borrow Id is required");
        }
    }
}