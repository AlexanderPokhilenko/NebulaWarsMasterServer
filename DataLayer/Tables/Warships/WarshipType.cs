using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    [Table("WarshipTypes")]
    public class WarshipType
    {
        private readonly ILazyLoader lazyLoader;
        public WarshipType()
        {
        }
        public WarshipType(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Name { get; set; }
        
        private List<Warship> warships;
        public virtual List<Warship> Warships 
        {
            get => lazyLoader.Load(this, ref warships);
            set => warships = value;
        }
    }
}