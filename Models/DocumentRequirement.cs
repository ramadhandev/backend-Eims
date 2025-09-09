using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class DocumentRequirement
    {
        [Key]
        public int DocumentRequirementId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Optional: list of training/card requirements
    }
}
