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
        [Required] public int WarshipExperience { get; set; }
        
        [Required] public int WarshipLevel { get; set; }
        
        [ForeignKey("AccountId")] public Account Account { get; set; }
        [ForeignKey("WarshipId")] public WarshipType WarshipType { get; set; }
    }
}