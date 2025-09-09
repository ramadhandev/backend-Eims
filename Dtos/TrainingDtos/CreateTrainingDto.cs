using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.TrainingDtos
{
    public class CreateTrainingDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string TrainingName { get; set; } = string.Empty;

        [Required]
        public DateTime CompletionDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public string? CertificateURL { get; set; }
        public string? Notes { get; set; }
    }
}
