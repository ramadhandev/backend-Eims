namespace EIMS.Models
{
    public class CardTypeDocumentRequirement
    {
        public int CardTypeId { get; set; }
        public CardType CardType { get; set; } = null!;

        public int DocumentRequirementId { get; set; }
        public DocumentRequirement DocumentRequirement { get; set; } = null!;

        public bool IsRequired { get; set; } = true;
    }
}
