using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Account.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required.")
                 .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().MinimumLength(6).WithMessage("New password must be at least 6 characters.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().MinimumLength(6).WithMessage("New password must be at least 6 characters.")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.");
        }
    }
}
