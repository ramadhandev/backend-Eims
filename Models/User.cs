using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty; // Pekerja, Supervisor, HSE, Manager

        [Required]
        public string Department { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Active";

        // Navigation
        public ICollection<HseCard> HseCards { get; set; } = new List<HseCard>();
        public ICollection<Training> Trainings { get; set; } = new List<Training>();
        public ICollection<PermitToWork> Permits { get; set; } = new List<PermitToWork>();
        public ICollection<Approval> Approvals { get; set; } = new List<Approval>();
        public ICollection<IncidentReport> IncidentReports { get; set; } = new List<IncidentReport>();
        public ICollection<UserDocument>? UserDocuments { get; set; }

    }
}
