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
        [Required] public WarshipTypeEnum WarshipTypeId { get; set; }

        public Account Account { get; set; }
        public WarshipType WarshipType { get; set; }
        
        public List<MatchResult> MatchResults{ get; set; } = new List<MatchResult>();
        public List<Increment> Increments { get; set; }
        public List<Decrement> Decrements { get; set; }
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{GetType().Name} ");
            stringBuilder.Append($"{nameof(Id)} {Id} ");
            stringBuilder.Append($"{nameof(WarshipTypeId)} {WarshipTypeId} ");
            // stringBuilder.Append($"{nameof(PowerLevel)} {PowerLevel} ");
            // stringBuilder.Append($"{nameof(PowerPoints)} {PowerPoints} ");
            // stringBuilder.Append($"{nameof(WarshipRating)} {WarshipRating} ");
            stringBuilder.Append($"{nameof(MatchResults)} {MatchResults?.Count} ");
            return stringBuilder.ToString();
        }
    }
}