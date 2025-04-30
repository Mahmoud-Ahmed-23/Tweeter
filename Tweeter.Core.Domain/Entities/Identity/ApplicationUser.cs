using Microsoft.AspNetCore.Identity;

namespace Tweeter.Core.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public required string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? ResetCode { get; set; }
        public DateTime? ResetCodeExpiry { get; set; }
    }
}
