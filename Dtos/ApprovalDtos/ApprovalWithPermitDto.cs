using EIMS.Dtos.PermitToWorkDtos;

namespace EIMS.Dtos.ApprovalDtos
{
    public class ApprovalWithPermitDto
    {
        public int ApprovalId { get; set; }
        public int PermitId { get; set; }
        public int ApproverId { get; set; }
        public string ApproverName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Decision { get; set; } = string.Empty;
        public DateTime DecisionDate { get; set; }
        public string? Comment { get; set; }
        public PermitBasicDto? PermitData { get; set; }
    }
}
