using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    [Table("warship_combat_roles")]
    public class WarshipCombatRole
    {        
        [Column("id")] [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Column("name")] [Required] public string Name { get; set; }
        
        public List<WarshipType> WarshipTypes { get; set; }
    }
}