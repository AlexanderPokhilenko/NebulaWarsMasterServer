using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class TestPurchaseConfiguration:IEntityTypeConfiguration<TestPurchase>
    {
        public void Configure(EntityTypeBuilder<TestPurchase> builder)
        {
            //Внешний ключ на аккаунт, который сделал покупку
            builder
                .HasOne(purchase => purchase.Account)
                .WithMany(account => account.Purchases)
                .HasForeignKey(purchase => purchase.AccountId);
            
            //Уникальность OrderId
            builder
                .HasIndex(purchase => purchase.OrderId)
                .IsUnique();
        }
    }
}