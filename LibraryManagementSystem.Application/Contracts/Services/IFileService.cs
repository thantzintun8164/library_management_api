using LibraryManagementSystem.Application.Common.DTOs;
using LibraryManagementSystem.Application.Common.Results;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementSystem.Application.Contracts.Services
{
    public interface IFileStorageService
    {
        Task<Result<MediaDto>> UploadImageAsync(IFormFile file);
        Task<Result<MediaDto>> UploadVideoAsync(IFormFile file);
        Task<Result<MediaDto>> UploadFileAsync(IFormFile file);
        Result Remove(string url);
        Task<Result<byte[]>> DownloadFileAsync(string url);
    }
}
