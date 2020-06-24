using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class Order
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public OrderTypeEnum OrderTypeId { get; set; }
        public Account Account { get; set; }
        public OrderType OrderType { get; set; }
        public List<Product> Products { get; set; }
    }
}