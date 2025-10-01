using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Account.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");


            RuleFor(x => x.NewPassword)
                .NotEmpty().MinimumLength(6).WithMessage("New password must be at least 6 characters.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().MinimumLength(6).WithMessage("Confirm New password must be at least 6 characters.")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
        }
    }
}
