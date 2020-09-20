using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration.Constraints
{
    public class SkinTypeConfiguration:IEntityTypeConfiguration<SkinType>
    {
        public void Configure(EntityTypeBuilder<SkinType> builder)
        {
            builder
                .HasOne(skinType => skinType.WarshipType)
                .WithMany(warshipType => warshipType.SkinTypes)
                .HasForeignKey(skinType => skinType.WarshipTypeId);
        }
    }
}