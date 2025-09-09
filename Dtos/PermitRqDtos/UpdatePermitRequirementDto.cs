namespace EIMS.Dto.PermitRqDto
{
    public class UpdatePermitRequirementDto
    {
        public string? WorkType { get; set; }
        public int? RequiredCardTypeId { get; set; }
        public string? RequiredTraining { get; set; }
        public string? Notes { get; set; }
    }
}
