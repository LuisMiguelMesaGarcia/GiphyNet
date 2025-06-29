using GifAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GifAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<SearchQueryWord> SearchQueryWords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<SearchHistory>()
                .HasIndex(s => s.SearchDate);

            modelBuilder.Entity<SearchQueryWord>()
                .HasOne(w => w.SearchHistory)
                .WithMany(s => s.QueryWords)
                .HasForeignKey(w => w.SearchHistoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SearchQueryWord>()
                .HasIndex(w => w.Word);

            // por si s ehace una busqueda por id
            modelBuilder.Entity<SearchQueryWord>()
                .HasIndex(w => new { w.SearchHistoryId, w.WordOrder });
        }
    }
}
