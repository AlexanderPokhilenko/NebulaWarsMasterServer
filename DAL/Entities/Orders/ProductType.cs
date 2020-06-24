using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class ProductType
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public ProductTypeEnum Id { get; set; }
        [Required] public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}