using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.ApprovalDtos
{
    public class UpdateApprovalDto
    {
        [Required]
        public string Decision { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}
