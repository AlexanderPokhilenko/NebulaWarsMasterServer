using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class LootboxesConfiguration:IEntityTypeConfiguration<LootboxDb>
    {
        public void Configure(EntityTypeBuilder<LootboxDb> builder)
        {
            builder
                .HasOne(w => w.Account)
                .WithMany(a => a.Lootboxes)
                .HasForeignKey(warship => warship.AccountId);
        }
    }
}