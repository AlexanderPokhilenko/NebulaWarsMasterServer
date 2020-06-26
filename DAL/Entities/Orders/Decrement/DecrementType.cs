using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class DecrementType
    {
        [Key] public DecrementTypeEnum Id { get; set; }
        [Required] public string Name { get; set; }
    }
}