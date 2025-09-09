using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class HseCard
    {
        [Key]
        public int CardId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        public int CardTypeId { get; set; }
        public CardType? CardType { get; set; }

        [Required]
        public int IssuedBy { get; set; }
        public User? IssueByUser { get; set; }

        public DateTime IssuedDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        [Required]
        public string Status { get; set; } = "Active";
        public string? Notes { get; set; }
    }
}
