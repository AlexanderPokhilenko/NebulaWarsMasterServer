using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    public class WarshipCombatRole
    {        
        [Key] public WarshipCombatRoleEnum Id { get; set; }
        [Required] public string Name { get; set; }
        
        public List<WarshipType> WarshipTypes { get; set; }
    }
}