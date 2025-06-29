using System.ComponentModel.DataAnnotations;

namespace GifAPI.Models.Domain
{
    public class SearchQueryWord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SearchHistoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Word { get; set; }

        [Required]
        public int WordOrder { get; set; }

        public virtual SearchHistory SearchHistory { get; set; }
    }
}
