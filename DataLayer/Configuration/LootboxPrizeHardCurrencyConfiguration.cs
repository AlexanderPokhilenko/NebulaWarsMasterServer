using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class LootboxPrizeHardCurrencyConfiguration:IEntityTypeConfiguration<LootboxPrizeHardCurrency>
    {
        public void Configure(EntityTypeBuilder<LootboxPrizeHardCurrency> builder)
        {
            builder
                .HasOne(w => w.LootboxDb)
                .WithMany(lootbox => lootbox.LootboxPrizeHardCurrency )
                .HasForeignKey(warship => warship.LootboxId);
        }
    }
}