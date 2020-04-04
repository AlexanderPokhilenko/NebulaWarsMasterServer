using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public struct MatchReward
    {
        public int WarshipRatingDelta { get; set; }
        public int RegularCurrencyDelta { get; set; }
        public int PremiumCurrencyDelta { get; set; }
        public int PointsForBigChest { get; set; }
        public int PointsForSmallChest { get; set; }
        public string JsonMatchResultDetails { get; set; }
    }
    [Table("MatchResultForPlayers")]
    public class MatchResultForPlayer
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int MatchId { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public int WarshipId { get; set; }
        
        
        public int? WarshipRatingDelta { get; set; }
        public int? RegularCurrencyDelta { get; set; }
        public int? PremiumCurrencyDelta { get; set; }
        public int? PointsForBigChest { get; set; }
        public int? PointsForSmallChest { get; set; }
        public int? PlaceInMatch { get; set; }
        public string JsonMatchResultDetails { get; set; }
        
        [ForeignKey("MatchId")] public Match Match { get; set; }
        [ForeignKey("AccountId")] public Account Account { get; set; }
        [ForeignKey("WarshipId")] public Warship Warship { get; set; }
    }
}