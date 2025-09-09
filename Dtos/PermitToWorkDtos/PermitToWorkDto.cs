

using EIMS.Dtos.ApprovalDtos;
using EIMS.Models;

namespace EIMS.Dtos.PermitToWorkDtos
{
    public class PermitToWorkDto
    {
        public int PermitId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string WorkType { get; set; } = string.Empty;
        public string? Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? RequiredCardTypeId { get; set; }
        public string? RequiredCardTypeName { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool AutoApproved { get; set; }
        public DateTime? AutoApprovedDate { get; set; }
        public int? AutoApprovedBy { get; set; }
        public string? AutoApprovedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Notes { get; set; }
        public List<ApprovalDto> Approvals { get; set; } = new List<ApprovalDto>();
    }
}
