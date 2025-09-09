using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class Approval
    {
        [Key]
        public int ApprovalId { get; set; }

        [Required]
        public int PermitId { get; set; }
        public PermitToWork? Permit { get; set; }

        [Required]
        public int ApproverId { get; set; }
        public User? Approver { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty; // Supervisor, HSE, Manager

        [Required]
        public string Decision { get; set; } = "Pending"; // Approved, Rejected

        public DateTime DecisionDate { get; set; }
        public string? Comment { get; set; }
    }
}
