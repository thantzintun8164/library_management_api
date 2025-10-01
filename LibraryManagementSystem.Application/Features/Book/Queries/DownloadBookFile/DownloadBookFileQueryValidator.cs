using FluentValidation;

namespace LibraryManagementSystem.Application.Features.Book.Queries.DownloadBookFile
{
    public class DownloadBookFileQueryValidator : AbstractValidator<DownloadBookFileQuery>
    {
        public DownloadBookFileQueryValidator()
        {
            RuleFor(x => x.BookId)
                  .NotEmpty().WithMessage("Book Id is required")
                  .GreaterThan(0).WithMessage("Book Id Must be Greater than 0");
        }
    }
}
