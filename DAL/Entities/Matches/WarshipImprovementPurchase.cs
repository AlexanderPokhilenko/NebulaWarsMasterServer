// using System;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
//
// namespace DAL.Tables
// {
//     [Table("warship_improvement_purchases")]
//     public class WarshipImprovementPurchase
//     {
//         [Column("id")] [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
//         
//         [Column("warshipId")] [Required] public int WarshipId { get; set; }
//         [Column("regularCurrencyCost")] [Required] public int RegularCurrencyCost { get; set; }
//         [Column("spentPowerPoints")] [Required] public int SpentPowerPoints { get; set; }
//         [Column("obtainedPowerLevel")] [Required] public int ObtainedPowerLevel { get; set; }
//         [Column("dateTime")] [Required] public CreationDateTime CreationDateTime { get; set; }
//         
//         [ForeignKey("WarshipId")] public virtual Warship Warship { get; set; }
//     }
// }