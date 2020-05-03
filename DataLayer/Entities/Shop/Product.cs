using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class Product
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public ProductTypeEnum ProductType { get; set; }
        [Required] public int KitId { get; set; }
        public string JsonPayload { get; set; }
    }
}