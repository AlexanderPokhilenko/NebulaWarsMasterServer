using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class LootboxPrizeWarshipPowerPoints
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int LootboxId { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public int WarshipId { get; set; }
        
        public LootboxDb LootboxDb { get; set; }
    }
}