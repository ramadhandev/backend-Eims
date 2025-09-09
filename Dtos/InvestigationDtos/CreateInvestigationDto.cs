using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.InvestigationDtos
{
    public class CreateInvestigationDto
    {
        [Required]
        public int IncidentId { get; set; }

        [Required]
        public int HSEOfficerId { get; set; }

        public string? RootCause { get; set; }
        public string? CorrectiveAction { get; set; }
        public string? PreventiveAction { get; set; }
    }
}
