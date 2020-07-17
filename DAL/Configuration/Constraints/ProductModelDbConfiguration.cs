using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration.Constraints
{
    public class ShopModelDbConfiguration:IEntityTypeConfiguration<ShopModelDb>
    {
        public void Configure(EntityTypeBuilder<ShopModelDb> builder)
        {
            builder
                .HasOne(order => order.Account)
                .WithMany(account => account.ShopModels)
                .HasForeignKey(order => order.AccountId);
        }
    }
}