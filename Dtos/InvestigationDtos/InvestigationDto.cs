namespace EIMS.Dtos.InvestigationDtos
{
    public class InvestigationDto
    {
        public int InvestigationId { get; set; }
        public string IncidentNumber { get; set; } = string.Empty;
        public int IncidentId { get; set; }
        public string IncidentTitle { get; set; } = string.Empty; // Tambahkan
        public int HSEOfficerId { get; set; }
        public string HSEOfficerName { get; set; } = string.Empty;
        public string? RootCause { get; set; }
        public string? CorrectiveAction { get; set; }
        public string? PreventiveAction { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
