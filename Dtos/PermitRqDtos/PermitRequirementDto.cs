namespace EIMS.Dto.PermitRqDto
{
    public class PermitRequirementDto
    {
        public int RequirementId { get; set; }
        public string WorkType { get; set; } = string.Empty;
        public int RequiredCardTypeId { get; set; }
        public string RequiredCardTypeName { get; set; } = string.Empty;
        public string? RequiredTraining { get; set; }
        public string? Notes { get; set; }
    }
}
