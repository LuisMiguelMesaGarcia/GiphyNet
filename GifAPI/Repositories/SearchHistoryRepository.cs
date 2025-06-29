using GifAPI.Data;
using GifAPI.Models.Domain;
using GifAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GifAPI.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public SearchHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SearchHistory>> GetAllAsync()
        {
            return await _context.SearchHistories
                .Include(s => s.QueryWords.OrderBy(w => w.WordOrder))
                .OrderByDescending(s => s.SearchDate)
                .ToListAsync();
        }
public async Task<SearchHistory> AddAsync(SearchHistory searchHistory)
        {
            await _context.SearchHistories.AddAsync(searchHistory);
            await _context.SaveChangesAsync();
            return searchHistory;
        }
        

    }
}
