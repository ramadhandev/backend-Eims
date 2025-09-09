namespace EIMS.Dto.PermitToWorkDtos
{
    public class UpdatePermitToWorkDto
    {
        public string? Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? RequiredCardTypeId { get; set; }
        public string? Notes { get; set; }
    }
}
