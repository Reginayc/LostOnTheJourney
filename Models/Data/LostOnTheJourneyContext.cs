using Microsoft.EntityFrameworkCore;

namespace LOSTONTHEJOURNEY.Models.Data
{
    public class LostOnTheJourneyContext : DbContext
    {
        public LostOnTheJourneyContext(DbContextOptions<LostOnTheJourneyContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
