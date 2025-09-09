using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.UserDocumentDto.UserDocumentDto
{
    public class CreateUserDocumentDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int DocumentRequirementId { get; set; }

        [Required]
        public string? FileURL { get; set; }
    }
}
