using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class DecrementsConfiguration:IEntityTypeConfiguration<Decrement>
    {
        public void Configure(EntityTypeBuilder<Decrement> builder)
        {
            builder
                .HasOne(decr => decr.Transaction)
                .WithMany(transaction => transaction.Decrements)
                .HasForeignKey(decr => decr.ResourceId);
            
            builder
                .HasOne(decrement => decrement.Warship)
                .WithMany(warship => warship.Decrements)
                .HasForeignKey(decrement => decrement.WarshipId)
                .IsRequired(false);

        }
    }
}