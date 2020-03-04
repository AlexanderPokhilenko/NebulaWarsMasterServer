using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("MatchResults")]
    public class MatchResult
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int MatchId { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public int WarshipId { get; set; }
        
        [Required] public int AccountExperienceDelta { get; set; }
        [Required] public int WarshipExperienceDelta { get; set; }
        [Required] public int RegularCurrencyDelta { get; set; }
        [Required] public int PremiumCurrency { get; set; }
        [Required] public int PointsForBigChest { get; set; }
        [Required] public int PointsForSmallChest { get; set; }
        [Required] public int MatchRating { get; set; }
        
        [ForeignKey("MatchId")] public FinishedMatch Match { get; set; }
        [ForeignKey("AccountId")] public Account Account { get; set; }
        [ForeignKey("WarshipId")] public Warship Warship { get; set; }
    }
}