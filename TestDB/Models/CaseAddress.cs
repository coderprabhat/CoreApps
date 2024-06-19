namespace TestDB.Models
{
    public class CaseAddress
    {
        public Guid Id { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }

        // Foreign key for the one-to-one relationship
        public Guid CaseId { get; set; }
        public Case? Case { get; set; }
    }
}
