namespace EIMS.Dtos.InvestigationDtos
{
    public class UpdateInvestigationDto
    {
        public string? RootCause { get; set; }
        public string? CorrectiveAction { get; set; }
        public string? PreventiveAction { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
