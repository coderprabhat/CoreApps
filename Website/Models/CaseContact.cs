using System.Text.Json.Serialization;

namespace Website.Models
{
    public class CaseContact
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("roles")]
        public string? Roles { get; set; }

        [JsonPropertyName("responsibilities")]
        public string? Responsibilities { get; set; }

        [JsonPropertyName("contactCategory")]
        public string? ContactCategory { get; set; }

        [JsonPropertyName("caseId")]
        public Guid CaseId { get; set; }

        [JsonIgnore]
        public List<string> SelectedRoles { get; set; } = new List<string>();

        [JsonIgnore]
        public List<string> SelectedResponsibilities { get; set; } = new List<string>();
    }
}
