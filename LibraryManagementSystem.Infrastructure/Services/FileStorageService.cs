using LibraryManagementSystem.Application.Common.DTOs;
using LibraryManagementSystem.Application.Common.Results;
using LibraryManagementSystem.Application.Contracts.Services;
using LibraryManagementSystem.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystem.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly FileStorageSettings _settings;
        private readonly IAppEnvironment _env;
        public FileStorageService(IOptions<FileStorageSettings> settings, IAppEnvironment env)
        {
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings)); ;
            _env = env ?? throw new ArgumentNullException(nameof(env)); ;
        }

        public Task<Result<MediaDto>> UploadImageAsync(IFormFile file) =>
            UploadAsync(file, _settings.ImageExtentionAllowed, _settings.ImageSizeInMB, _settings.ImagesFolder);

        public Task<Result<MediaDto>> UploadVideoAsync(IFormFile file) =>
            UploadAsync(file, _settings.VideoExtentionAllowed, _settings.VideoSizeInMB, _settings.VideosFolder);
        public Task<Result<MediaDto>> UploadFileAsync(IFormFile file) =>
            UploadAsync(file, _settings.FileExtentionAllowed, _settings.FileSizeInMB, _settings.FilesFolder);
        public Result Remove(string url)
        {
            var fullPath = Path.Combine(_env.WebRootPath, url);
            if (!File.Exists(fullPath))
                return Result.Fail($"file not exist");
            File.Delete(fullPath);
            return Result.Success();
        }
        public async Task<Result<byte[]>> DownloadFileAsync(string url)
        {
            var fullPath = Path.Combine(_env.WebRootPath, url);
            if (!File.Exists(fullPath))
                return Result<byte[]>.Fail("File not exist");

            var fileBytes = await File.ReadAllBytesAsync(fullPath);
            return Result<byte[]>.Success(fileBytes);
        }
        private async Task<Result<MediaDto>> UploadAsync(IFormFile file, string[] extentionAllowed, int maxSizeInMB, string folderName)
        {
            var validationResult = ValidateFile(file, extentionAllowed, maxSizeInMB);
            if (!validationResult.IsSuccess)
                return Result<MediaDto>.Fail(validationResult.Message);

            var folderPath = EnsureFolder(folderName);
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            string newName = $"{Guid.NewGuid()}{extension}";
            string fullPath = Path.Combine(folderPath, newName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(fileStream);

            var mediaDto = new MediaDto
            {
                ContentType = file.ContentType,
                FileUrl = $"{_settings.UploadsFolder}/{folderName}/{newName}",
            };
            return Result<MediaDto>.Success(mediaDto);
        }
        private Result ValidateFile(IFormFile file, string[] allowedExtensions, int maxSizeInMB)
        {
            if (file == null || file.Length == 0)
                return Result.Fail("File cannot be null or empty.");

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Any(e => e.Equals(extension, StringComparison.OrdinalIgnoreCase)))
                return Result.Fail($"Extension '{extension}' is not allowed.");

            if (file.Length > maxSizeInMB * 1024 * 1024)
                return Result.Fail($"File size exceeds {maxSizeInMB}MB.");

            return Result.Success();
        }
        private string EnsureFolder(string folderName)
        {
            string path = Path.Combine(_env.WebRootPath, _settings.UploadsFolder, folderName);
            Directory.CreateDirectory(path);
            return path;
        }

    }
}
