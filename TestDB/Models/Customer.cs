namespace TestDB.Models
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
        public ShippingAddress? ShippingAddress { get; set; }
        public BillingAddress? BillingAddress { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
    }
}
