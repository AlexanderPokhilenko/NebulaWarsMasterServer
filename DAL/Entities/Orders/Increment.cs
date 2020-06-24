using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class Increment
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int ProductId { get; set; }
        [Required] public IncrementTypeEnum IncrementTypeId { get; set; }
        public int SoftCurrency { get; set; }
        public int HardCurrency { get; set; }
        public int LootboxPowerPoints { get; set; }
        public int WarshipId { get; set; }
        public int WarshipPowerPoints { get; set; } 
        public Product Product { get; set; }
        public IncrementType IncrementType { get; set; }
    }
}