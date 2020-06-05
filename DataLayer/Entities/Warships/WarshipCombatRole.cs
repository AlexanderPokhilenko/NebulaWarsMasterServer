using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    public class WarshipCombatRole
    {        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Name { get; set; }
        
        public List<WarshipType> WarshipTypes { get; set; }
    }
}