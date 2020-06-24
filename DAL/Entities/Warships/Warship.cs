using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    public class Warship
    { 
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public int WarshipTypeId { get; set; }

        public Account Account { get; set; }
        public WarshipType WarshipType { get; set; }
        
        public List<MatchResultForPlayer> MatchResultForPlayers{ get; set; } = new List<MatchResultForPlayer>();

        public List<LootboxPrizeWarshipPowerPoints> WarshipPowerPoints { get; set; } = new List<LootboxPrizeWarshipPowerPoints>();

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{GetType().Name} ");
            stringBuilder.Append($"{nameof(Id)} {Id} ");
            stringBuilder.Append($"{nameof(WarshipTypeId)} {WarshipTypeId} ");
            // stringBuilder.Append($"{nameof(PowerLevel)} {PowerLevel} ");
            // stringBuilder.Append($"{nameof(PowerPoints)} {PowerPoints} ");
            // stringBuilder.Append($"{nameof(WarshipRating)} {WarshipRating} ");
            stringBuilder.Append($"{nameof(MatchResultForPlayers)} {MatchResultForPlayers?.Count} ");
            return stringBuilder.ToString();
        }
    }
}