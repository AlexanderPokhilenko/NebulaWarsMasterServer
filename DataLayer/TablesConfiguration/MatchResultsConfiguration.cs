using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class MatchResultsConfiguration:IEntityTypeConfiguration<MatchResultForPlayer>
    {
        public void Configure(EntityTypeBuilder<MatchResultForPlayer> builder)
        {
            builder
                .HasIndex(r => new { r.AccountId, r.MatchId })
                .IsUnique();
            
            builder
                .HasOne(matchResult => matchResult.Warship)
                .WithMany(warship => warship.MatchResultForPlayers);
            
            builder
                .HasOne(playerMatchResult => playerMatchResult.Match)
                .WithMany(match =>  match.MatchResultForPlayers)
                .HasForeignKey(playerMatchResult => playerMatchResult.MatchId);
        }
    }
}
