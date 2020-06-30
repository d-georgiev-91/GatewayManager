using GatewayManager.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using WebSiteManager.DataModels;

namespace GatewayManager.Data
{
    public class GatewayManagerDbContext : DbContext
    {
        public GatewayManagerDbContext(DbContextOptions<GatewayManagerDbContext> options) :
            base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GatewayManager;Trusted_Connection=True;MultipleActiveResultSets=true");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Gateway> Gateways { get; set; }

        public DbSet<PeripheralDevice> PeripheralDevice { get; set; }
    }
}
