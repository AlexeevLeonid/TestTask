using Microsoft.EntityFrameworkCore;
using Test66bit2.src.Model;

namespace Test66bit2.src.DbClients.EntityClient
{
    public class EntityDbContext : DbContext
    {
        private IServiceScopeFactory Services { get; }
        public DbSet<Soccer> Soccers { get; set; } = null!;

        public List<string> countries = new List<string>() { "USA", "Russia", "Italy" };
        public List<string> sex = new List<string>() { "Male", "Female" };
        public EntityDbContext(DbContextOptions<EntityDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();
        }
    }
}
