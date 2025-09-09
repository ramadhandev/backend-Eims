using System.ComponentModel.DataAnnotations;

namespace EIMS.Dto.IncidentDto
{
    public class CreateIncidentReportDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Location { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? PhotoURL { get; set; }
    }
}
