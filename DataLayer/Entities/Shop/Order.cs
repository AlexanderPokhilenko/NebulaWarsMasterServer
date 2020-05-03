using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    public class Order
    {
        private readonly ILazyLoader lazyLoader;
        public Order()
        {
        }
        public Order(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public int KitId { get; set; }
        [Required] public DateTime CreationDate  { get; set; }
        
        [ForeignKey("AccountId")] public virtual Account Account { get; set; }
        [ForeignKey("KitId")] public virtual Kit Kit { get; set; }
    }
}