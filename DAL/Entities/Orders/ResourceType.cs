using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class ResourceType
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public ResourceTypeEnum Id { get; set; }
        [Required] public string Name { get; set; }
        public List<Resource> Products { get; set; }
    }
}