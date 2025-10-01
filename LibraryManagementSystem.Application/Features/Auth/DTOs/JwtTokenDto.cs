namespace LibraryManagementSystem.Application.Features.Auth.DTOs
{
    public class JwtTokenDto
    {
        public string AccessToken { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}
