using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Tables;

namespace DataLayer.Entities.Transactions.Decrement
{
    public class RealPurchaseModel
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public string Data {get;set; }
        [Required] public string Sku {get;set; }
        public Account Account { get; set; }
    }
}