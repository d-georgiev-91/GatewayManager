using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GatewayManager.DataModels;

namespace GatewayManager.Data.Configurations
{
    public class PeripheralDeviceConfiguration : IEntityTypeConfiguration<PeripheralDevice>
    {
        public void Configure(EntityTypeBuilder<PeripheralDevice> entity)
        {
            entity.HasKey(e => e.Uid);

            entity.Property(e => e.IsOnline)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(e => e.Vendor)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Gateway)
                .WithMany(p => p.PeripheralDevices)
                .HasForeignKey(d => d.GatewaySerialNumber);
        }
    }
}
