using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Account.Commands.RemoveFromRole
{
    internal class RemoveFromRoleCommandValidator : AbstractValidator<RemoveFromRoleCommand>
    {
        public RemoveFromRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Invalid role specified.");
        }
    }
}
