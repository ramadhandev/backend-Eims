namespace EIMS.Dtos.TrainingDtos
{
    public class UpdateTrainingDto
    {
        public string? TrainingName { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CertificateURL { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }
}
