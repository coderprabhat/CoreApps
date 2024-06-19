using System.Text.Json;
using System.Text;
using Website.Models;

namespace Website.Services
{
    public class CaseContactService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5134/api/casecontacts";

        public CaseContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CaseContact>> GetAllCaseContactsAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<CaseContact>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<CaseContact> GetCaseContactByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CaseContact>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task CreateCaseContactAsync(CaseContact newCaseContact)
        {
            var jsonRequest = JsonSerializer.Serialize(newCaseContact);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCaseContactAsync(Guid id, CaseContact updatedCaseContact)
        {
            var jsonRequest = JsonSerializer.Serialize(updatedCaseContact);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);
            response.EnsureSuccessStatusCode();
        }


        public async Task DeleteCaseContactAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<string>> GetRolesForCaseAsync(Guid caseId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/roles/{caseId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<string>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<CaseContact>> GetCaseContactsByCaseIdAsync(Guid caseId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/bycase/{caseId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<CaseContact>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

    }
}
