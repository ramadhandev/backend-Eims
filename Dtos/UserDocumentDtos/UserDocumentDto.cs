using EIMS.Dtos.UserDocumentDto.UserDocumentDto;
using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.UserDocumentDtos
{
    public class UserDocumentDto
    {
        public int UserDocumentId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int DocumentRequirementId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        public string? FileURL { get; set; }
    }

    public class CreateUserDocumentWithFileDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int DocumentRequirementId { get; set; }

        public string? FileURL { get; set; }

        public IFormFile? File { get; set; }

        public CreateUserDocumentDto ToCreateDto()
        {
            return new CreateUserDocumentDto
            {
                UserId = UserId,
                DocumentRequirementId = DocumentRequirementId,
                FileURL = FileURL
            };
        }
    }


    public class UpdateUserDocumentWithFileDto
    {
        public string? FileURL { get; set; }

        public IFormFile? File { get; set; }

        public UpdateUserDocumentDto ToUpdateDto()
        {
            return new UpdateUserDocumentDto
            {
                FileURL = FileURL
            };
        }
    }
}
