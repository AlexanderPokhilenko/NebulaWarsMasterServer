using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required] public int SoftCurrency { get; set; }
        [Required] public int HardCurrency { get; set; }
        [Required] public int LootboxPoints { get; set; }
        [Required] public int WarshipId { get; set; }
        [Required] public int WarshipPowerPoints { get; set; }
        [Required] public int WarshipRating { get; set; }
        
        public Resource Resource { get; set; }
        public IncrementType IncrementType { get; set; }
    }
}