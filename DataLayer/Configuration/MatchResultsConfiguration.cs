using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class MatchResultsConfiguration:IEntityTypeConfiguration<MatchResultForPlayer>
    {
        public void Configure(EntityTypeBuilder<MatchResultForPlayer> builder)
        {
            //В одном матче один корабль может учавствовать толко один раз
            builder
                .HasIndex(r => new { r.WarshipId, r.MatchId })
                .IsUnique();
            
            //У каждого корабля может быть много результатов матчей
            builder
                .HasOne(matchResultForPlayer => matchResultForPlayer.Warship)
                .WithMany(warship => warship.MatchResultForPlayers)
                .HasForeignKey(matchResultForPlayer => matchResultForPlayer.WarshipId);
            
            builder
                .HasOne(matchResultForPlayer => matchResultForPlayer.Match)
                .WithMany(match => match.MatchResultForPlayers)
                .HasForeignKey(matchResultForPlayer => matchResultForPlayer.MatchId);
        }
    }
}
