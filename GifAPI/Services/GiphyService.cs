using GifAPI.Models.DTO;
using GifAPI.Services.Interface;
using System.Text.Json;

namespace GifAPI.Services
{
    public class GiphyService : IGiphyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GiphyService> _logger;
        private const string ApiKey = "voaNIOg1u7ONPbckzWK71C48YqCOkhVP";

        public GiphyService(HttpClient httpClient, ILogger<GiphyService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetGifAsync(string query, int offsetNumber)
        {
            try
            {
                var encodedQuery = Uri.EscapeDataString(query);
                var url = $"https://api.giphy.com/v1/gifs/search?api_key={ApiKey}&q={encodedQuery}&limit=1&offset={offsetNumber}";

                var response = await _httpClient.GetStringAsync(url);
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var giphyResponse = JsonSerializer.Deserialize<GiphyResponseDto>(response, options);

                return giphyResponse?.Data?.FirstOrDefault()?.Images?.Original?.Url ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching gif from Giphy");
                throw;
            }
        }
    }
}
