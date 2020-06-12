using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Tables
{
    public class MatchResultForPlayer
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int MatchId { get; set; }
        [Required] public int WarshipId { get; set; }
        
        [Required] public bool WasShown { get; set; }
        [Required] public bool IsFinished { get; set; }
        [Required] public int WarshipRatingDelta { get; set; }
        [Required] public int SoftCurrencyDelta { get; set; }
        [Required] public int BigLootboxPoints { get; set; }
        [Required] public int SmallLootboxPoints { get; set; }
        [Required] public int PlaceInMatch { get; set; }
        
        public Match Match { get; set; }
        public Warship Warship { get; set; }
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{GetType().Name} ");
            stringBuilder.Append($"{nameof(Id)} {Id} ");
            stringBuilder.Append($"{nameof(MatchId)} {MatchId} ");
            stringBuilder.Append($"{nameof(WarshipId)} {WarshipId} ");
            stringBuilder.Append($"{nameof(WasShown)} {WasShown} ");
            stringBuilder.Append($"{nameof(WarshipRatingDelta)} {WarshipRatingDelta} ");
            stringBuilder.Append($"{nameof(SoftCurrencyDelta)} {SoftCurrencyDelta} ");
            stringBuilder.Append($"{nameof(BigLootboxPoints)} {BigLootboxPoints} ");
            stringBuilder.Append($"{nameof(SmallLootboxPoints)} {SmallLootboxPoints} ");
            stringBuilder.Append($"{nameof(PlaceInMatch)} {PlaceInMatch} ");
            return stringBuilder.ToString();
        }
    }
}