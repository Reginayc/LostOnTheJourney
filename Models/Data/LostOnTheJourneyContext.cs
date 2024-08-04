using Microsoft.EntityFrameworkCore;
using LOSTONTHEJOURNEY.Models;

namespace LOSTONTHEJOURNEY.Models.Data
{
    public class LostOnTheJourneyContext : DbContext
    {
        public LostOnTheJourneyContext(DbContextOptions<LostOnTheJourneyContext> options)
            : base(options)
        {
            Movies = Set<Movie>(); // Initialize the DbSet
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
