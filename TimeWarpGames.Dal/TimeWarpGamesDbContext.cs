using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Dal
{
    public class TimeWarpGamesDbContext: DbContext
    {
        public TimeWarpGamesDbContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Console> Consoles { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
    }
}