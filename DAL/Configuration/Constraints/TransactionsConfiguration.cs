using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration.Constraints
{
    public class TransactionsConfiguration:IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasOne(order => order.Account)
                .WithMany(account => account.Transactions)
                .HasForeignKey(order => order.AccountId);
            
            builder
                .HasOne(order => order.TransactionType)
                .WithMany(orderType => orderType.Orders)
                .HasForeignKey(order => order.TransactionTypeId);
        }
    }
}