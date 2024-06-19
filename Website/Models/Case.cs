using System.Text.Json.Serialization;

namespace Website.Models
{
    public class Case
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("legalBusinessName")]
        public string? LegalBusinessName { get; set; }

        [JsonPropertyName("businessTaxId")]
        public string? BusinessTaxId { get; set; }

        [JsonPropertyName("plans")]
        public string? Plans { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime? CreatedOn { get; set; }

        [JsonPropertyName("updatedOn")]
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public List<string> SelectedPlans { get; set; } = new List<string>();
    }
}
