using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Account.Commands.AddToRole
{
    public class AddToRoleCommandValidator : AbstractValidator<AddToRoleCommand>
    {
        public AddToRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Invalid role specified.");
        }
    }
}
