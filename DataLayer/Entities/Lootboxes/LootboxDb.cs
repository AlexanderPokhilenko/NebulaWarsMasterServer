// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.EntityFrameworkCore.Infrastructure;
//
// namespace DataLayer.Tables
// {
//     [Table("Lootboxes")]
//     public class LootboxDb
//     {
//         private readonly ILazyLoader lazyLoader;
//         public LootboxDb()
//         {
//         }
//         public LootboxDb(ILazyLoader lazyLoader)
//         {
//             this.lazyLoader = lazyLoader;
//         }
//         
//         [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
//         [Required] public int AccountId { get; set; }
//         [Required] public LootboxType LootboxType { get; set; }
//         [Required] public DateTime CreationDate { get; set; }
//         [Required] public bool WasShown { get; set; }
//         
//         [ForeignKey("AccountId")] public virtual Account Account { get; set; }
//         
//         private List<LootboxPrizeRegularCurrency> lootboxPrizeRegularCurrencies;
//         public virtual List<LootboxPrizeRegularCurrency> LootboxPrizeRegularCurrencies
//         {
//             get => lazyLoader.Load(this, ref lootboxPrizeRegularCurrencies);
//             set => lootboxPrizeRegularCurrencies = value;
//         }
//         
//         private List<LootboxPrizePointsForSmallLootbox> lootboxPrizePointsForSmallChests;
//         public virtual List<LootboxPrizePointsForSmallLootbox> LootboxPrizePointsForSmallChests
//         {
//             get => lazyLoader.Load(this, ref lootboxPrizePointsForSmallChests);
//             set => lootboxPrizePointsForSmallChests = value;
//         }
//         
//         private List<LootboxPrizeWarshipPowerPoints> lootboxPrizeWarshipPowerPoints;
//         public virtual List<LootboxPrizeWarshipPowerPoints> LootboxPrizeWarshipPowerPoints
//         {
//             get => lazyLoader.Load(this, ref lootboxPrizeWarshipPowerPoints);
//             set => lootboxPrizeWarshipPowerPoints = value;
//         }
//     }
// }