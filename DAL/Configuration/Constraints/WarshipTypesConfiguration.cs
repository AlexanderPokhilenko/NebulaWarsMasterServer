using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration.Constraints
{
    public class WarshipTypesConfiguration:IEntityTypeConfiguration<WarshipType>
    {
        public void Configure(EntityTypeBuilder<WarshipType> builder)
        {
            builder
                .HasOne(w => w.WarshipCombatRole)
                .WithMany(a => a.WarshipTypes)
                .HasForeignKey(warship => warship.WarshipCombatRoleId);
        }
    }
}