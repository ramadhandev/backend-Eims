using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class IncidentReport
    {
        [Key]
        public int IncidentId { get; set; }
        public string IncidentNumber { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime Date { get; set; }
        public string? Location { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty; // Near Miss, Accident, Unsafe Condition

        public string? Description { get; set; }
        public string? PhotoURL { get; set; }
        public string? Status { get; set; } = "Reported";

        public Investigation? Investigation { get; set; }
    }
}
