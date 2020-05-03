using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    [Table("WarshipCombatRoles")]
    public class WarshipCombatRole
    {
        private readonly ILazyLoader lazyLoader;
        public WarshipCombatRole()
        {
        }
        public WarshipCombatRole(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Name { get; set; }
        
        private List<WarshipType> warshipTypes;
        public virtual List<WarshipType> WarshipTypes 
        {
            get => lazyLoader.Load(this, ref warshipTypes);
            set => warshipTypes = value;
        }
    }
}