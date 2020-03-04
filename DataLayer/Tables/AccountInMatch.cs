using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("AccountsInMatches")]
    public class AccountInMatch
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public int WarshipId { get; set; }
        [Required] public int RunningMatchId { get; set; }
    }
}