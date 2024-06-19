using System.Text.Json;
using System.Text;
using Website.Models;

namespace Website.Services
{
    public class CaseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5134/api/cases";

        public CaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Case>> GetAllCasesAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Case>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Case> GetCaseByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Case>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task CreateCaseAsync(Case newCase)
        {
            var jsonRequest = JsonSerializer.Serialize(newCase);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCaseAsync(Guid id, Case updatedCase)
        {
            var jsonRequest = JsonSerializer.Serialize(updatedCase);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCaseAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
