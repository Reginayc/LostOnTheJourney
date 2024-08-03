using Microsoft.EntityFrameworkCore;
using LOSTONTHEJOURNEY.API.Models;

namespace LOSTONTHEJOURNEY.API.Data
{
    public class LostOnTheJourneyContext : DbContext
    {
        public LostOnTheJourneyContext(DbContextOptions<LostOnTheJourneyContext> options)
            : base(options)
        {
        }

        public DbSet<Movie>? Movies { get; set; }
    }
}
