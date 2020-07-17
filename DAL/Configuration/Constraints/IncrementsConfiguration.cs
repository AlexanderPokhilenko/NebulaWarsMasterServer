using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration.Constraints
{
    public class IncrementsConfiguration:IEntityTypeConfiguration<Increment>
    {
        public void Configure(EntityTypeBuilder<Increment> builder)
        {
            builder
                .HasOne(inc => inc.Transaction)
                .WithMany(tran => tran.Increments)
                .HasForeignKey(inc => inc.TransactionId);
            
            builder
                .HasOne(increment => increment.Warship)
                .WithMany(warship => warship.Increments)
                .HasForeignKey(increment => increment.WarshipId)
                .IsRequired(false);
            
            builder
                .HasOne(increment => increment.MatchRewardType)
                .WithMany(matchRewardType => matchRewardType.Increments)
                .HasForeignKey(increment => increment.MatchRewardTypeId)
                .IsRequired(false);

            builder
                .HasOne(increment => increment.IncrementType)
                .WithMany(incrementType => incrementType.Increments)
                .HasForeignKey(increment => increment.IncrementTypeId);
            
            builder
                .HasOne(increment => increment.SkinType)
                .WithMany(skinType => skinType.Increments)
                .HasForeignKey(increment => increment.SkinTypeId)
                .IsRequired(false);
        }
    }
}