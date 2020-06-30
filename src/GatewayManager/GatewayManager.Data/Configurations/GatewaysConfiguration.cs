using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteManager.DataModels;

namespace GatewayManager.Data.Configurations
{
    public class GatewaysConfiguration : IEntityTypeConfiguration<Gateway>
    {
        public void Configure(EntityTypeBuilder<Gateway> entity)
        {
            entity.HasKey(e => e.SerialNumber);

            entity.Property(e => e.IPv4Address)
                .IsRequired()
                .HasMaxLength(15);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
