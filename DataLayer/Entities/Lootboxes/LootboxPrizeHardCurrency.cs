using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class LootboxPrizeHardCurrency
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int LootboxId { get; set; }
        [Required] public int Quantity { get; set; }
        public LootboxDb LootboxDb { get; set; }
    }
}