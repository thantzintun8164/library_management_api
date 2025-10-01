using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Account.Queries.GetUserById
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .Matches("^[a-fA-F0-9]{24}$").WithMessage("Invalid User ID format.");
        }
    }
}
