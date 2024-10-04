using KartverketApiApp.Models;
using KartverketApiApp.Services;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace KartverketApiApp.Services
{
    public class KommuneInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<KommuneInfoService> _logger;

        public KommuneInfoService(HttpClient httpClient, ILogger<KommuneInfoService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.kartverket.no/kommuneinfo/v1/kommuner/{kommuneNr}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"KommuneInfo Response: {json}");
                var kommuneInfo = JsonSerializer.Deserialize<KommuneInfo>(json);
                return kommuneInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching KommuneInfo for {kommuneNr}: {ex.Message}");
                return null;
            }
        }
    }
}
