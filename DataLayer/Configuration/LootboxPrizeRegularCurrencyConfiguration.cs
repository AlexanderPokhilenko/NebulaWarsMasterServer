using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class LootboxPrizeRegularCurrencyConfiguration:IEntityTypeConfiguration<LootboxPrizeRegularCurrency>
    {
        public void Configure(EntityTypeBuilder<LootboxPrizeRegularCurrency> builder)
        {
            builder
                .HasOne(w => w.LootboxDb)
                .WithMany(lootbox => lootbox.LootboxPrizeRegularCurrencies )
                .HasForeignKey(warship => warship.LootboxId);
        }
    }
}