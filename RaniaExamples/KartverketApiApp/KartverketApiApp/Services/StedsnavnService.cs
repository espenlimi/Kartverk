using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using KartverketApiApp.Models;

namespace KartverketApiApp.Services
{

    // Her  definerer vi en tjeneste kalt StedsnavnService som bruker en HTTP-klient til å hente stednavn-data fra Kartverkets API, og en logger (ILogger) for å logge meldinger og feil.
    public class StedsnavnService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StedsnavnService> _logger;


        public StedsnavnService(HttpClient httpClient, ILogger<StedsnavnService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // GetStedsnavnAsync-metoden: Sender en GET-forespørsel til Kartverket API med en søkestreng.
        // Den sikrer at forespørselen var vellykket og deserialiserer JSON-svaret til en StedsnavnResponse-modell.
        // Logger både API-respons og eventuelle feil som oppstår.
        public async Task<StedsnavnResponse> GetStedsnavnAsync(string search)
        {
            try
            {
                // Her venter "await" på at HTTP-forespørselen skal fullføres.
                var response = await _httpClient.GetAsync($"https://api.kartverket.no/stedsnavn/v1/navn?sok={search}");

                // Sjekker om HTTP-forespørselen var vellykket ved å verifisere statuskoden fra svaret.
                // Hvis statuskoden er i området 200–299 (som indikerer suksess), fortsetter koden uten avbrudd.
                // Hvis statuskoden er utenfor dette området (for eksempel 400 eller 500), kaster den en HttpRequestException.
                response.EnsureSuccessStatusCode();

                //  Venter på at responsens innhold skal leses som en streng
                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Stedsnavn Response: {json}");

                // Konverterer en JSON-streng (som er lagret i variabelen json) til et C#-objekt av typen StedsnavnResponse.
                // JSON-strengen representerer data fra API-svaret.
                var stedsnavnResponse = JsonSerializer.Deserialize<StedsnavnResponse>(json);
                return stedsnavnResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching Stedsnavn for '{search}': {ex.Message}");
                return null;
            }

        }
    }
}
