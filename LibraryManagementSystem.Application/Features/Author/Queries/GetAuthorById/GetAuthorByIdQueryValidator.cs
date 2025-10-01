using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Author.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdQueryValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("Author Id is required.")
                .GreaterThan(0).WithMessage("Author Id must be greater than zero.");
        }
    }
}
