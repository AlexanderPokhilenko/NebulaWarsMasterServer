using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    public class LootboxPrizeWarshipPowerPoints
    {
        private readonly ILazyLoader lazyLoader;
        public LootboxPrizeWarshipPowerPoints()
        {
        }
        public LootboxPrizeWarshipPowerPoints(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int LootboxId { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public int WarshipId { get; set; }

        [ForeignKey("LootboxId")]
        public virtual LootboxDb LootboxDb { get; set; }
    }
}