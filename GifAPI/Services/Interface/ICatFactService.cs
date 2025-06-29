using GifAPI.Models.DTO;

namespace GifAPI.Services.Interface
{
    public interface ICatFactService
    {
        Task<CatFactDto> GetRandomFactAsync();
    }
}
