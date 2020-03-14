using System.Collections.Generic;
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
    
    public class WarshipTypeConfiguration:IEntityTypeConfiguration<WarshipType>
    {
        public void Configure(EntityTypeBuilder<WarshipType> builder)
        {
            builder.HasData(new List<WarshipType>()
            {
                new WarshipType{Id = 1,Name = "hare"},
                new WarshipType{Id = 2,Name = "bird"}
            });
        }
    }
}