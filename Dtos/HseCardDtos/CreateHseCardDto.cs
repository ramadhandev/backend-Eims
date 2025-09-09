using System.ComponentModel.DataAnnotations;

namespace EIMS.Dtos.HseCardDtos
{
    public class CreateHseCardDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        public int CardTypeId { get; set; }

        [Required]
        public int IssuedBy { get; set; }

        [Required]
        public DateTime IssuedDate { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        public string? Notes { get; set; }
    }
}
