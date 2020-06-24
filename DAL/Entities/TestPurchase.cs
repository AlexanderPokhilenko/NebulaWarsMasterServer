// using System;
// using System.ComponentModel.DataAnnotations;
// using DataLayer.Tables;
//
// namespace DataLayer
// {
//     public class TestPurchase
//     {
//         [Key] public int Id { get; set; }
//         [Required] public int AcknowledgementState { get; set; }
//         [Required] public int ConsumptionState { get; set; }
//         [Required] public string DeveloperPayload { get; set; }
//         [Required] public string Kind { get; set; }
//         [Required] public string OrderId { get; set; }
//         [Required] public int PurchaseState { get; set; }
//         [Required] public long PurchaseTimeMillis { get; set; }
//         public int? PurchaseType { get; set; }
//         [Required] public string Sku { get; set; }
//         [Required] public string Token { get; set; }
//         [Required] public int AccountId { get; set; }
//         [Required] public bool IsConfirmed { get; set; } 
//         [Required] public DateTime DateTime { get; set; }
//         [Required] public string Data { get; set; }
//         
//         public Account Account { get; set; }
//     }
// }