using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Account.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.FullName)
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.")
                .MinimumLength(2).WithMessage("Full name must be greather than two characters.")
                .When(x => x.FullName != null);

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid enum value for gender.")
                .When(x => x.Gender.HasValue);

        }
    }
}
