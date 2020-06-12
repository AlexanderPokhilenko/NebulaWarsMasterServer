using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class LootboxPrizePointsForSmallLootboxConfiguration:IEntityTypeConfiguration<LootboxPrizeSmallLootboxPoints>
    {
        public void Configure(EntityTypeBuilder<LootboxPrizeSmallLootboxPoints> builder)
        {
            builder
                .HasOne(w => w.LootboxDb)
                .WithMany(lootbox => lootbox.LootboxPrizePointsForSmallLootboxes )
                .HasForeignKey(warship => warship.LootboxId);
        }
    }
}