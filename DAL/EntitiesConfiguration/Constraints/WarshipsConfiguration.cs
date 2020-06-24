using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class WarshipsConfiguration:IEntityTypeConfiguration<Warship>
    {
        public void Configure(EntityTypeBuilder<Warship> builder)
        {
            //У аккаунта не может быть несколько одинаковых типов кораблей
            builder
                .HasIndex(r => new { r.AccountId, r.WarshipTypeId })
                .IsUnique();
            
            //У аккаунта может быть много кораблей
            builder
                .HasOne(w => w.Account)
                .WithMany(a => a.Warships)
                .HasForeignKey(warship => warship.AccountId);
            
            //У каждого типа корабля может быть много экземпляров
            builder
                .HasOne(warship => warship.WarshipType)
                .WithMany(warshipType => warshipType.Warships)
                .HasForeignKey(warship => warship.WarshipTypeId);
        }
    }
}