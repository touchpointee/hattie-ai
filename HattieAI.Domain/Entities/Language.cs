using System.ComponentModel.DataAnnotations;

namespace HattieAI.Domain.Entities
{
    public class Language
    {
        [Key]
        [MaxLength(50)]
        public string Code { get; set; } // ISO code, e.g., "en", "fr-FR"

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } // Display name, e.g., "English", "French"

        public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    }
}
