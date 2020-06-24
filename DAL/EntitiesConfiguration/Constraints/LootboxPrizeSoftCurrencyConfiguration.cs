// using DataLayer.Tables;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace DataLayer.TablesConfiguration
// {
//     public class LootboxPrizeSoftCurrencyConfiguration:IEntityTypeConfiguration<LootboxPrizeSoftCurrency>
//     {
//         public void Configure(EntityTypeBuilder<LootboxPrizeSoftCurrency> builder)
//         {
//             builder
//                 .HasOne(w => w.LootboxDb)
//                 .WithMany(lootbox => lootbox.LootboxPrizeSoftCurrency )
//                 .HasForeignKey(warship => warship.LootboxId);
//         }
//     }
// }