using FluentValidation;
using LibraryManagementSystem.Application.Settings;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystem.Application.Features.Book.Commands.UpdateBookFile
{
    public class UpdateBookFileCommandValidator : AbstractValidator<UpdateBookFileCommand>
    {
        private readonly FileStorageSettings _settings;
        public UpdateBookFileCommandValidator(IOptions<FileStorageSettings> options)
        {
            _settings = options.Value;
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Book Id is required")
                .GreaterThan(0).WithMessage("Book Id must be greater than 0");

            RuleFor(x => x.NewFile)
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
