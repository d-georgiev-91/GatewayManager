using System;
using System.Linq;
using Humanizer;
using Microsoft.EntityFrameworkCore;

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
    }
}
