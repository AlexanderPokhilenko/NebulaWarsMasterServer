using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class MatchResultsConfiguration:IEntityTypeConfiguration<PlayerMatchResult>
    {
        public void Configure(EntityTypeBuilder<PlayerMatchResult> builder)
        {
            builder
                .HasIndex(r => new { r.AccountId, r.MatchId })
                .IsUnique();
        }
    }
}
