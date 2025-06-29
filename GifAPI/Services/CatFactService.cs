using GifAPI.Models.DTO;
using GifAPI.Services.Interface;
using System.Text.Json;

namespace GifAPI.Services
{
    public class CatFactService : ICatFactService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatFactService> _logger;

        public CatFactService(HttpClient httpClient, ILogger<CatFactService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CatFactDto> GetRandomFactAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://catfact.ninja/fact");
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var catFact = JsonSerializer.Deserialize<CatFactDto>(response, options);
                return catFact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching cat fact");
                throw;
            }
        }
    }
}
