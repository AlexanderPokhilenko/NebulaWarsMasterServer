using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("Accounts")]
    public class Account
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int RegularCurrency { get; set; }
        [Required] public int PremiumCurrency { get; set; }
        [Required] public int PointsForBigChest { get; set; }
        [Required] public int PointsForSmallChest { get; set; }
        [Required] public int Experience { get; set; }
    }
}