using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class Training
    {
        [Key]
        public int TrainingId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public string TrainingName { get; set; } = string.Empty;

        public DateTime CompletionDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public string? CertificateURL { get; set; }

        [Required]
        public string Status { get; set; } = "InProgress"; // Completed, Expired, InProgress
        public string? Notes { get; set; }
    }
}
