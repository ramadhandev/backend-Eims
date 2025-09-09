using System.ComponentModel.DataAnnotations;

namespace EIMS.Dto.PermitRqDto
{
    public class CreatePermitRequirementDto
    {

        [Required]
        public string WorkType { get; set; } = string.Empty;

        [Required]
        public int RequiredCardTypeId { get; set; }

        public string? RequiredTraining { get; set; }
        public string? Notes { get; set; }
    }
}
