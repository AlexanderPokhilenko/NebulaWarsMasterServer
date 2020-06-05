using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Tables
{
    public class Account
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string ServiceId { get; set; }
        [Required] public string Username { get; set; }
        [Required] public DateTime CreationDate { get; set; }
        
        
        [NotMapped] public int RegularCurrency { get; set; }
        [NotMapped] public int PremiumCurrency { get; set; }
        [NotMapped] public int PointsForSmallLootbox { get; set; }
        [NotMapped] public int Rating { get; set; }

        
        public List<Warship> Warships { get; set; } = new List<Warship>();
        public List<LootboxDb> Lootboxes { get; set; } = new List<LootboxDb>();
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{GetType().Name} ");
            stringBuilder.Append($"{nameof(Id)} {Id} ");
            stringBuilder.Append($"{nameof(ServiceId)} {ServiceId} ");
            stringBuilder.Append($"{nameof(RegularCurrency)} {RegularCurrency} ");
            stringBuilder.Append($"{nameof(PremiumCurrency)} {PremiumCurrency} ");
            stringBuilder.Append($"{nameof(PointsForSmallLootbox)} {PointsForSmallLootbox} ");
            stringBuilder.Append($"{nameof(CreationDate)} {CreationDate} ");
            stringBuilder.Append($"{nameof(Rating)} {Rating} ");
            stringBuilder.Append($"warshipsCount {Warships?.Count} ");
            stringBuilder.Append($"lootboxesCount {Lootboxes?.Count} ");
            return stringBuilder.ToString();
        }
    }
}