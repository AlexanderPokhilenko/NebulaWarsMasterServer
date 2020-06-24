using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class OrderType
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public OrderTypeEnum Id { get; set; }
        [Required] public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}