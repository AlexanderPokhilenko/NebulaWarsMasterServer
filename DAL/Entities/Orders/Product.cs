using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class Product
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int OrderId { get; set; }
        [Required] public ProductTypeEnum ProductTypeId { get; set; }
        public Order Order { get; set; }
        public ProductType ProductType { get; set; }
        public List<Increment> Increments { get; set; }
        public List<Decrement> Decrements { get; set; }
    }
}