using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    [Table("Accounts")]
    public class Account
    {
        private readonly ILazyLoader lazyLoader;
        public Account()
        {
        }
        public Account(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public string ServiceId { get; set; }
        [Required] public string Username { get; set; }
        [NotMapped] public int RegularCurrency { get; set; }
        [NotMapped] public int PremiumCurrency { get; set; }
        [Required] public int PointsForSmallLootbox { get; set; }
        [Required] public DateTime CreationDate { get; set; }
        [NotMapped] public int Rating { get; set; }

        private List<Warship> warships;
        public virtual List<Warship> Warships
        {
            get => lazyLoader.Load(this, ref warships);
            set => warships = value;
        }
        
        private List<LootboxDb> lootboxes;
        public virtual List<LootboxDb> Lootboxes
        {
            get => lazyLoader.Load(this, ref lootboxes);
            set => lootboxes = value;
        }
    }
}