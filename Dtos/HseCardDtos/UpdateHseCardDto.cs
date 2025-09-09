namespace EIMS.Dtos.HseCardDtos
{
    public class UpdateHseCardDto
    {
        public string? CardNumber { get; set; }
        public int? CardTypeId { get; set; }
        public int? IssuedBy { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }
}
