namespace GifAPI.Models.DTO
{
    public class GiphyResponseDto
    {
        public List<GiphyDataDto>? Data { get; set; }
    }

    public class GiphyDataDto
    {
        public GiphyImagesDto? Images { get; set; }
    }

    public class GiphyImagesDto
    {
        public GiphyImageDto? Original { get; set; }
    }

    public class GiphyImageDto
    {
        public string? Url { get; set; }
    }
}
