using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class PermitRequirement
    {
        [Key]
        public int RequirementId { get; set; }

        [Required]
        public string WorkType { get; set; } = string.Empty;

        [Required]
        public int RequiredCardTypeId { get; set; }
        public CardType? RequiredCardType { get; set; }

        public string? RequiredTraining { get; set; } // optional, bisa comma-separated
        public string? Notes { get; set; }
    }
}
