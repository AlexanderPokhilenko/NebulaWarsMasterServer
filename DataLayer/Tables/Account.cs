using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("Accounts")]
    public class Account
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public string ServiceId { get; set; }
        [Required] public string Username { get; set; }
        [Required] public int RegularCurrency { get; set; }
        [Required] public int PremiumCurrency { get; set; }
        [Required] public int PointsForBigChest { get; set; }
        [Required] public int PointsForSmallChest { get; set; }
        [Required] public DateTime RegistrationDate { get; set; }

        public List<Warship> Warships { get; set; }
    }
    
    [Table("Purchases")]
    public class Purchase
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Json { get; set; }
        public long PurchaseTimeMillis{ get; set; }
        public int PurchaseState{ get; set; }
        public int ConsumptionState{ get; set; }
        public string DeveloperPayload{ get; set; }
        public string OrderId{ get; set; }
        public int PurchaseType{ get; set; }
        public int AcknowledgementState{ get; set; }
        public string Kind{ get; set; }
        
        
    }
}