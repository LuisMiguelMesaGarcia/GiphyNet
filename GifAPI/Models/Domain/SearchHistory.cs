using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GifAPI.Models.Domain
{
    public class SearchHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime SearchDate { get; set; }

        [Required]
        [MaxLength(1000)]
        public string CatFact { get; set; }

        [Required]
        [MaxLength(500)]
        public string GifUrl { get; set; }

        public virtual ICollection<SearchQueryWord> QueryWords { get; set; } = new List<SearchQueryWord>();

        [NotMapped]
        public string QueryWordsAsString => string.Join(' ', QueryWords.OrderBy(w => w.WordOrder).Select(w => w.Word));
    }
}
