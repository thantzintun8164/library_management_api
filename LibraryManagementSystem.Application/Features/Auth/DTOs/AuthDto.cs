using LibraryManagementSystem.Domain.Entities;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Application.Features.Auth.DTOs
{
    public class AuthDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }//for access token

        [JsonIgnore]
        public RefreshToken RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
