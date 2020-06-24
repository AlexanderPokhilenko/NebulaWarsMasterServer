using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class Decrement
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int ProductId { get; set; }
        public int SoftCurrency { get; set; }
        public int HardCurrency { get; set; }
        public int LootboxPowerPoints { get; set; }
        [Required] public DecrementTypeEnum DecrementTypeId { get; set; }
        public string RealPurchaseData { get; set; }
        public Product Product { get; set; }
        public DecrementType DecrementType { get; set; }
    }
}