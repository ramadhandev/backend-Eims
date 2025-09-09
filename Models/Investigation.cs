using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class Investigation
    {
        [Key]
        public int InvestigationId { get; set; }

        [Required]
        public int IncidentId { get; set; }
        public IncidentReport? Incident { get; set; }

        [Required]
        public int HSEOfficerId { get; set; }
        public User? HSEOfficer { get; set; }

        public string? RootCause { get; set; }
        public string? CorrectiveAction { get; set; }
        public string? PreventiveAction { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
