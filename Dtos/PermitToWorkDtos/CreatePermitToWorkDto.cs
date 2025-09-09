using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.PermitToWorkDtos
{
    public class CreatePermitToWorkDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string WorkType { get; set; } = string.Empty;

        public string? Location { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? RequiredCardTypeId { get; set; }
        public string? Notes { get; set; }
    }
}
