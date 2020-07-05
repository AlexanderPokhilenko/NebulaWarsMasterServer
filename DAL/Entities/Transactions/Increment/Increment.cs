using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.NetworkLibrary.Experimental;

namespace DataLayer.Tables
{
    /// <summary>
    /// Все значения положительны.
    /// </summary>
    public class Increment
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int ResourceId { get; set; }
        [Required] public IncrementTypeEnum IncrementTypeId { get; set; }
        [Required] public int Amount { get; set; }
        public int? WarshipId { get; set; }
        public MatchRewardTypeEnum? MatchRewardTypeId { get; set; } 
        public MatchRewardType MatchRewardType { get; set; }
        public Transaction Transaction { get; set; }
        public IncrementType IncrementType { get; set; }
        public Warship Warship { get; set; }
    }
}