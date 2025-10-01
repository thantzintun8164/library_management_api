namespace LibraryManagementSystem.Application.Common.DTOs
{
    public class DownLoadFileDto
    {
        public byte[] FileBytes { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public string DownloadName { get; set; } = null!;
    }
}
