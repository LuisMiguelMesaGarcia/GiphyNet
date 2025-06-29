namespace GifAPI.Services.Interface
{
    public interface IGiphyService
    {
        Task<string> GetGifAsync(string query,int offsetNumber);
    }
}
