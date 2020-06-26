using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class Decrement
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int ResourceId { get; set; }
        [Required] public int SoftCurrency { get; set; }
        [Required] public int HardCurrency { get; set; }
        [Required] public int LootboxPoints { get; set; }
        [Required] public DecrementTypeEnum DecrementTypeId { get; set; }

        [Required] public int WarshipRating {get;set; }
        public int? WarshipId {get;set;}
        public string RealPurchaseData { get; set;}
        public Warship Warship { get; set; }
        
        public Resource Resource { get; set; }
        public DecrementType DecrementType { get; set; }
    }
}