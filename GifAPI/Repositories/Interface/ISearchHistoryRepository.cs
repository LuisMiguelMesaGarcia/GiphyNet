using GifAPI.Models.Domain;

namespace GifAPI.Repositories.Interface
{
    public interface ISearchHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> GetAllAsync();
        Task<SearchHistory> AddAsync(SearchHistory searchHistory);
    }
}
