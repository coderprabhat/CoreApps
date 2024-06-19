using System.Text.Json.Serialization;

namespace TestDB.Models
{
    public class BillingAddress
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.Now;

        public Guid CustomerId { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }

    }
}
