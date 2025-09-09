namespace EIMS.Dtos.HseCardDtos
{
    public class HseCardDto
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public int CardTypeId { get; set; }
        public string CardTypeName { get; set; } = string.Empty;
        public int IssuedBy { get; set; }
        public string IssuedByName { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
