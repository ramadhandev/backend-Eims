namespace EIMS.Dtos.TrainingDtos
{
    public class TrainingDto
    {
        public int TrainingId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string TrainingName { get; set; } = string.Empty;
        public DateTime CompletionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? CertificateURL { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
