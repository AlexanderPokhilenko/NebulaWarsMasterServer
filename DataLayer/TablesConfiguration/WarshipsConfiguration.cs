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
        }
    }
}