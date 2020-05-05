using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("MatchResultForPlayers")]
    public class MatchResultForPlayer
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int MatchId { get; set; }
        [Required] public int WarshipId { get; set; }
        
        [Required] public bool WasShown { get; set; }
        
        public int? WarshipRatingDelta { get; set; }
        public int? RegularCurrencyDelta { get; set; }
        public int? PremiumCurrencyDelta { get; set; }
        public int? PointsForBigChest { get; set; }
        public int? PointsForSmallChest { get; set; }
        public int? PlaceInMatch { get; set; }
        public string JsonMatchResultDetails { get; set; }
        
        
        [ForeignKey("MatchId")] public virtual Match Match { get; set; }
        [ForeignKey("WarshipId")] public virtual Warship Warship { get; set; }
    }
}