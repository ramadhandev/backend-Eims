namespace EIMS.Dtos.PermitToWorkDtos
{
    public class PermitBasicDto
    {
        public int PermitId { get; set; }
        public string WorkType { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
