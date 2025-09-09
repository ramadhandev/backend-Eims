using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class UserDocument
    {
        [Key]
        public int UserDocumentId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public int DocumentRequirementId { get; set; }
        public DocumentRequirement? DocumentRequirement { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public string? FileURL { get; set; }
    }
}
