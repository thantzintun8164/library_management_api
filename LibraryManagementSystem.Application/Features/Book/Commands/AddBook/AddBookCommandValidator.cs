using FluentValidation;
using LibraryManagementSystem.Application.Settings;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystem.Application.Features.Book.Commands.AddBook
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        private readonly FileStorageSettings _settings;
        public AddBookCommandValidator(IOptions<FileStorageSettings> options)
        {
            _settings = options.Value;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Title can't exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(4000).WithMessage("Title can't exceed 4000 characters.");

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(1500, DateTime.UtcNow.Year)
                .WithMessage($"Publication year must be between 1500 and {DateTime.UtcNow.Year}.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Author Id must be greather than 0");

            RuleFor(x => x.NumberOfAvailableBook)
                .GreaterThan(0).WithMessage("Number of available book must be greather than 0");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category Id must be greather than 0");

            RuleFor(x => x.BookFile)
              .NotNull().WithMessage("File is required")
              .Must(file => file?.Length > 0)
                  .WithMessage("File can't be empty")
              .Must(file => file != null && file.Length <= _settings.FileSizeInMB * 1024 * 1024)
                  .WithMessage($"File must be less than {_settings.FileSizeInMB}MB")
              .Must(file => file != null && _settings.FileExtentionAllowed
                  .Contains(Path.GetExtension(file.FileName)?.ToLowerInvariant()))
                  .WithMessage($"File type is not allowed. Allowed types: {string.Join(", ", _settings.FileExtentionAllowed)}");
        }
    }
}