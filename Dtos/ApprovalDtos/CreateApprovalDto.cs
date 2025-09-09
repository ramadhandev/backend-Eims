using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.ApprovalDtos
{
    public class CreateApprovalDto
    {
        [Required]
        public int PermitId { get; set; }

        [Required]
        public int ApproverId { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty;

        public string? Comment { get; set; }
    }
}
