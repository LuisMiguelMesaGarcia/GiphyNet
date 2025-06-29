using GifAPI.Models.DTO;

namespace GifAPI.Services.Interface
{
    public interface IFactGifService
    {
        //Task<FactGifResponseDto> GetFactWithGifAsync();
        Task<string> GetFactAsync(); 
        Task<string> GetNewGifForFactAsync(string fact);
        Task<IEnumerable<SearchHistoryDto>> GetSearchHistoryAsync();
    }
}
