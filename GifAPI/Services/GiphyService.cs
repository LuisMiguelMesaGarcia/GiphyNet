using GifAPI.Configurations;
using GifAPI.Models.DTO;
using GifAPI.Services.Interface;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace GifAPI.Services
{
    public class GiphyService : IGiphyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GiphyService> _logger;
        private readonly string _apiKey;

        public GiphyService(HttpClient httpClient, ILogger<GiphyService> logger, IOptions<GiphyOptions> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = options.Value.ApiKey;
        }

        public async Task<string> GetGifAsync(string query, int offsetNumber)
        {
            try
            {
                var encodedQuery = Uri.EscapeDataString(query);
                var url = $"https://api.giphy.com/v1/gifs/search?api_key={_apiKey}&q={encodedQuery}&limit=1&offset={offsetNumber}";

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
