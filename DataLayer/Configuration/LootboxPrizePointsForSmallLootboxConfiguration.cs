using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class LootboxPrizePointsForSmallLootboxConfiguration:IEntityTypeConfiguration<LootboxPrizePointsForSmallLootbox>
    {
        public void Configure(EntityTypeBuilder<LootboxPrizePointsForSmallLootbox> builder)
        {
            builder
                .HasOne(w => w.LootboxDb)
                .WithMany(lootbox => lootbox.LootboxPrizePointsForSmallLootboxes )
                .HasForeignKey(warship => warship.LootboxId);
        }
    }
}