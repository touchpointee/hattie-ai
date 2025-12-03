using HattieAI.Domain.Common;

namespace HattieAI.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Add other user properties as needed
    }
}
