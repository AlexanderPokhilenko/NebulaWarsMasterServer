using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    [Table("warships")]
    public class Warship
    { 
        [Column("id")] [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Column("account_id")] [Required] public int AccountId { get; set; }
        [Column("warship_type_id")] [Required] public int WarshipTypeId { get; set; }
        
        [NotMapped] public int PowerLevel { get; set; }
        [NotMapped] public int PowerPoints { get; set; }
        [NotMapped] public int Rating { get; set; }
        
        public Account Account { get; set; }
        public WarshipType WarshipType { get; set; }
        
        public List<MatchResultForPlayer> MatchResultForPlayers{ get; set; }
        // public List<WarshipImprovementPurchase> WarshipImprovementPurchases { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{GetType().Name} ");
            stringBuilder.Append($"{nameof(Id)} {Id} ");
            stringBuilder.Append($"{nameof(WarshipTypeId)} {WarshipTypeId} ");
            stringBuilder.Append($"{nameof(PowerLevel)} {PowerLevel} ");
            stringBuilder.Append($"{nameof(PowerPoints)} {PowerPoints} ");
            stringBuilder.Append($"{nameof(Rating)} {Rating} ");
            stringBuilder.Append($"{nameof(MatchResultForPlayers)} {MatchResultForPlayers?.Count} ");
            return stringBuilder.ToString();
        }
    }
}