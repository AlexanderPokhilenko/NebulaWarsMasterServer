using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class MatchResultsConfiguration:IEntityTypeConfiguration<MatchResult>
    {
        public void Configure(EntityTypeBuilder<MatchResult> builder)
        {
            //В одном матче один корабль может учавствовать толко один раз
            builder
                .HasIndex(r => new { r.WarshipId, r.MatchId })
                .IsUnique();
            
            //У каждого корабля может быть много результатов матчей
            builder
                .HasOne(matchResult => matchResult.Warship)
                .WithMany(warship => warship.MatchResults)
                .HasForeignKey(matchResult => matchResult.WarshipId);
            
            //У каждого матча может быть много результатов
            builder
                .HasOne(matchResult => matchResult.Match)
                .WithMany(match => match.MatchResults)
                .HasForeignKey(matchResult => matchResult.MatchId);
            
            //У результата матча может быть транзакция (если матч окончен)
            builder
                .HasOne(matchResult => matchResult.Transaction)
                .WithOne(transaction => transaction.MatchResult)
                .HasForeignKey<MatchResult>(matchResult => matchResult.TransactionId)
                .IsRequired(false);
        }
    }
}
