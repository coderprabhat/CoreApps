using System.Text.Json.Serialization;

namespace TestDB.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.Now;

        public Guid OrderId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
