namespace EIMS.Dto.IncidentDto
{
    public class IncidentReportDto
    {
        public int IncidentId { get; set; }
        public string IncidentNumber { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? PhotoURL { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
