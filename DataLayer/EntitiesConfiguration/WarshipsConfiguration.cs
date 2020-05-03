using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class WarshipsConfiguration:IEntityTypeConfiguration<Warship>
    {
        public void Configure(EntityTypeBuilder<Warship> builder)
        {
            builder
                .HasIndex(r => new { r.AccountId, r.WarshipTypeId })
                .IsUnique();
            
            builder
                .HasOne(w => w.Account)
                .WithMany(a => a.Warships);
            
            builder
                .HasOne(warship => warship.WarshipType)
                .WithMany(warshipType => warshipType.Warships)
                .HasForeignKey(warship => warship.WarshipTypeId);
        }
    }
}