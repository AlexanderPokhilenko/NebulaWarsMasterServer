using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class LootboxPrizeWarshipPowerPointsConfiguration:IEntityTypeConfiguration<LootboxPrizeWarshipPowerPoints>
    {
        public void Configure(EntityTypeBuilder<LootboxPrizeWarshipPowerPoints> builder)
        {
            builder
                .HasOne(w => w.LootboxDb)
                .WithMany(lootbox => lootbox.LootboxPrizeWarshipPowerPoints )
                .HasForeignKey(warship => warship.LootboxId);
            
            builder
                .HasOne(w => w.Warship)
                .WithMany(warship => warship.WarshipPowerPoints)
                .HasForeignKey(points =>  points.WarshipId);
        }
    }
}