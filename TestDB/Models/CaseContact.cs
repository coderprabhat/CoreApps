namespace TestDB.Models
{
    public class CaseContact
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Roles { get; set; }
        public string? Responsibilities { get; set; }
        public string? ContactCategory { get; set; }

        // Foreign key for the one-to-many relationship
        public Guid CaseId { get; set; }
        public Case? Case { get; set; }
    }
}
