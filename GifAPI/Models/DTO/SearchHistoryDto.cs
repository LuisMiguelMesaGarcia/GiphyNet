namespace GifAPI.Models.DTO
{
    public class SearchHistoryDto
    {
        public int Id { get; set; }
        public DateTime SearchDate { get; set; }
        public string CatFact { get; set; }
        public string QueryWords { get; set; }
        public string GifUrl { get; set; }
    }
}
