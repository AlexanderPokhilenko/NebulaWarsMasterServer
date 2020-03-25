using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("PlayerMatchResults")]
    public class PlayerMatchResult
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int MatchId { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public int WarshipId { get; set; }
        
        [Required] public int AccountRatingDelta { get; set; }
        [Required] public int WarshipRatingDelta { get; set; }
        [Required] public int RegularCurrencyDelta { get; set; }
        [Required] public int PremiumCurrencyDelta { get; set; }
        [Required] public int PointsForBigChest { get; set; }
        [Required] public int PointsForSmallChest { get; set; }
        public int? PlaceInMatch { get; set; }
        
        [ForeignKey("MatchId")] public Match Match { get; set; }
        [ForeignKey("AccountId")] public Account Account { get; set; }
        [ForeignKey("WarshipId")] public Warship Warship { get; set; }
    }
}