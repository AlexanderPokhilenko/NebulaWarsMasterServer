using DataLayer.Entities.Transactions.Decrement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration.Constraints
{
    public class RealPurchaseModelConfiguration:IEntityTypeConfiguration<RealPurchaseModel>
    {
        public void Configure(EntityTypeBuilder<RealPurchaseModel> builder)
        {
            //у аккаунта может быть много покупок
            builder
                .HasOne(realPurchaseModel => realPurchaseModel.Account)
                .WithMany(a => a.RealPurchaseModels)
                .HasForeignKey(realPurchaseModel => realPurchaseModel.AccountId);
        }
    }
}