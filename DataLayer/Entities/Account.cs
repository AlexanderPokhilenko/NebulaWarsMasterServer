using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    [Table("accounts")]
    public class Account
    {
        [Column("id")] [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Column("ServiceId")] [Required] public string ServiceId { get; set; }
        [Column("username")] [Required] public string Username { get; set; }
        [Column("regular_currency")] [Required] public int RegularCurrency { get; set; }
        [Column("premium_currency")] [Required] public int PremiumCurrency { get; set; }
        [Column("points_for_small_lootbox")] [Required] public int PointsForSmallLootbox { get; set; }
        [Column("creation_date")] [Required] public DateTime CreationDate { get; set; }
        [Column("rating")] [Required] public int Rating { get; set; }

        public List<Warship> Warships { get; set; }
        // public List<LootboxDb> Lootboxes{ get; set; }

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
            // stringBuilder.Append($"lootboxesCount {Lootboxes?.Count} ");
            return stringBuilder.ToString();
        }
    }
}