using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.NetworkLibrary.Experimental;
using NetworkLibrary.NetworkLibrary.Http;

namespace DataLayer.Tables
{
    /// <summary>
    /// Все значения положительны.
    /// </summary>
    public class Increment
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        [Required] public IncrementTypeEnum IncrementTypeId { get; set; }
        public IncrementType IncrementType { get; set; }
        [Required] public int Amount { get; set; }
        public int? WarshipId { get; set; }
        public Warship Warship { get; set; }
        public MatchRewardTypeEnum? MatchRewardTypeId { get; set; } 
        public MatchRewardType MatchRewardType { get; set; }
        public SkinTypeEnum? SkinTypeId { get; set; }
        public SkinType SkinType { get; set; }
    }
}