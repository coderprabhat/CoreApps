using System.Text.Json.Serialization;

namespace TestDB.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public IEnumerable<Product>? Products { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.Now;

        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}
