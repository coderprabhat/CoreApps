namespace TestDB.Models
{
    public class Case
    {
        public Guid Id { get; set; }
        public string? LegalBusinessName { get; set; }
        public string? BusinessTaxId { get; set; }
        public string? Plans { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}
