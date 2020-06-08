using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Tables
{
    public class AccountDbDto
    {
        public int Id { get; set; }
        public string ServiceId { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public int SoftCurrency { get; set; }
        public int HardCurrency { get; set; }
        public int SmallLootboxPoints { get; set; }
        public int Rating { get; set; }
        
        public List<WarshipDbDto> Warships { get; set; } = new List<WarshipDbDto>();
        public List<LootboxDb> Lootboxes { get; set; } = new List<LootboxDb>();
    }
    
    
    public class Account
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string ServiceId { get; set; }
        [Required] public string Username { get; set; }
        [Required] public DateTime RegistrationDateTime { get; set; }
        
        
        // [NotMapped] public int SoftCurrency { get; set; }
        // [NotMapped] public int HardCurrency { get; set; }
        // [NotMapped] public int SmallLootboxPoints { get; set; }
        // [NotMapped] public int Rating { get; set; }

        
        public List<Warship> Warships { get; set; } = new List<Warship>();
        public List<LootboxDb> Lootboxes { get; set; } = new List<LootboxDb>();
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{GetType().Name} ");
            stringBuilder.Append($"{nameof(Id)} {Id} ");
            stringBuilder.Append($"{nameof(ServiceId)} {ServiceId} ");
            // stringBuilder.Append($"{nameof(SoftCurrency)} {SoftCurrency} ");
            // stringBuilder.Append($"{nameof(HardCurrency)} {HardCurrency} ");
            // stringBuilder.Append($"{nameof(SmallLootboxPoints)} {SmallLootboxPoints} ");
            // stringBuilder.Append($"{nameof(Rating)} {Rating} ");
            stringBuilder.Append($"{nameof(RegistrationDateTime)} {RegistrationDateTime} ");
            stringBuilder.Append($"warshipsCount {Warships?.Count} ");
            stringBuilder.Append($"lootboxesCount {Lootboxes?.Count} ");
            return stringBuilder.ToString();
        }
    }
}