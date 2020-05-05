using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class WarshipImprovementPurchase
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int WarshipId { get; set; }
        [Required] public int RegularCurrencyCost { get; set; }
        [Required] public int ObtainedPowerLevel { get; set; }
        [Required] public DateTime DateTime { get; set; }
        [ForeignKey("WarshipId")] public virtual Warship Warship { get; set; }
    }
}