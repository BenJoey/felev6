using Microsoft.EntityFrameworkCore;

namespace Cinema.Models
{
    public class CinemaContext : DbContext
    {
        public CinemaContext (DbContextOptions<CinemaContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Seat> Seats { get; set; }
    }
}
