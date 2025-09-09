using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class PermitToWork
    {
        [Key]
        public int PermitId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public string WorkType { get; set; } = string.Empty;

        public string? Location { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? RequiredCardTypeId { get; set; }
        public CardType? RequiredCardType { get; set; }

        [Required]
        public string Status { get; set; } = "Requested"; // Requested, AutoApproved, PendingApproval, Approved, Rejected, Closed

        public bool AutoApproved { get; set; }
        public DateTime? AutoApprovedDate { get; set; }
        public int? AutoApprovedBy { get; set; }
        public User? AutoApprovedByUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }

        public ICollection<Approval> Approvals { get; set; } = new List<Approval>();
    }
}
