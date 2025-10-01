using LibraryManagementSystem.Application.Common.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Queries.DownloadBookFile
{
    public record DownloadBookFileQuery(int BookId) : IRequest<DownLoadFileDto>;
}
