using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class ShopModelDb
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public DateTime CreationDateTime { get; set; }
        public byte[] SerializedModel { get; set; }
        public Account Account { get; set; }
    }
}