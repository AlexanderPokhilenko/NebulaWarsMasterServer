using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("Warships")]
    public class Warship
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int AccountId { get; set; }
        [Required] public int WarshipTypeId { get; set; }
        
        [Required] public int WarshipCombatPowerLevel { get; set; }
        [Required] public int WarshipCombatPowerValue { get; set; }
        [Required] public int WarshipRating { get; set; }

        [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
        [ForeignKey(nameof(WarshipTypeId))] public WarshipType WarshipType { get; set; }
    }
}