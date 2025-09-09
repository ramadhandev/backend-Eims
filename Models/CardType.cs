using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class CardType
    {
        [Key]
        public int CardTypeId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<HseCard> HseCards { get; set; } = new List<HseCard>();
        public ICollection<PermitRequirement> PermitRequirements { get; set; } = new List<PermitRequirement>();
    }
}
