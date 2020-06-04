using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Tables
{
    [Table("match_result_for_players")]
    public class MatchResultForPlayer
    {
        [Column("id")][Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Column("match_id")] [Required] public int MatchId { get; set; }
        [Column("warship_id")] [Required] public int WarshipId { get; set; }
        
        [Column("was_shown")] [Required] public bool WasShown { get; set; }
        
        [Column("warship_rating_delta")] public int? WarshipRatingDelta { get; set; }
        [Column("regular_currency_delta")] public int? RegularCurrencyDelta { get; set; }
        [Column("premium_currency_delta")] public int? PremiumCurrencyDelta { get; set; }
        [Column("points_for_big_chest")] public int? PointsForBigChest { get; set; }
        [Column("points_for_small_chest")] public int? PointsForSmallChest { get; set; }
        [Column("place_in_match")] public int? PlaceInMatch { get; set; }
        
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
            stringBuilder.Append($"{nameof(RegularCurrencyDelta)} {RegularCurrencyDelta} ");
            stringBuilder.Append($"{nameof(PremiumCurrencyDelta)} {PremiumCurrencyDelta} ");
            stringBuilder.Append($"{nameof(PointsForBigChest)} {PointsForBigChest} ");
            stringBuilder.Append($"{nameof(PointsForSmallChest)} {PointsForSmallChest} ");
            stringBuilder.Append($"{nameof(PlaceInMatch)} {PlaceInMatch} ");
            return stringBuilder.ToString();
        }
    }
}