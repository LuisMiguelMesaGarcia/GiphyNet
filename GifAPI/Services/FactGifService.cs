using GifAPI.Models.Domain;
using GifAPI.Models.DTO;
using GifAPI.Repositories.Interface;
using GifAPI.Services.Interface;

namespace GifAPI.Services
{
    public class FactGifService : IFactGifService
    {
        private readonly ICatFactService _catFactService;
        private readonly IGiphyService _giphyService;
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public FactGifService(
            ICatFactService catFactService,
            IGiphyService giphyService,
            ISearchHistoryRepository searchHistoryRepository)
        {
            _catFactService = catFactService;
            _giphyService = giphyService;
            _searchHistoryRepository = searchHistoryRepository;
        }

        public async Task<string> GetFactAsync()
        {
            var fact = await _catFactService.GetRandomFactAsync();
            var queryWords = ExtractFirstThreeWords(fact.Fact);
            return queryWords;
        }

        public async Task<FactGifResponseDto> GetFactWithGifAsync()
        {
            var fact = await _catFactService.GetRandomFactAsync();
            var queryWords = ExtractFirstThreeWords(fact.Fact);
            var gifUrl = await _giphyService.GetGifAsync(queryWords);

            await SaveToHistory(fact.Fact, queryWords, gifUrl);

            return new FactGifResponseDto
            {
                Fact = fact.Fact,
                GifUrl = gifUrl,
                QueryWords = queryWords
            };
        }

        public async Task<string> GetNewGifForFactAsync(string? fact)
        {
            var queryWords = ExtractFirstThreeWords(fact);
            var gifUrl = await _giphyService.GetGifAsync(queryWords);

            await SaveToHistory(fact, queryWords, gifUrl);

            return gifUrl;
        }

        public async Task<IEnumerable<SearchHistoryDto>> GetSearchHistoryAsync()
        {
            var history = await _searchHistoryRepository.GetAllAsync();
            return history.Select(h => new SearchHistoryDto
            {
                Id = h.Id,
                SearchDate = h.SearchDate,
                CatFact = h.CatFact,
                QueryWords = h.QueryWordsAsString,
                GifUrl = h.GifUrl
            });
        }

        private string ExtractFirstThreeWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Join(' ', words.Take(3));
        }

        private async Task SaveToHistory(string fact, string queryWords, string gifUrl)
        {
            var searchHistory = new SearchHistory
            {
                SearchDate = DateTime.UtcNow,
                CatFact = fact,
                GifUrl = gifUrl
            };

            var words = queryWords.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                searchHistory.QueryWords.Add(new SearchQueryWord
                {
                    Word = words[i].Trim(),
                    WordOrder = i + 1
                });
            }

            await _searchHistoryRepository.AddAsync(searchHistory);
        }
    }
}
