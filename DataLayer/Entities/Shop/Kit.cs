using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    public class Kit
    {
        private readonly ILazyLoader lazyLoader;
        public Kit()
        {
        }
        public Kit(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
            
        }
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        // [Required] public CurrencyTypeEnum CurrencyType { get; set; }
        public decimal? Cost { get; set; }
        private int? MaximumAmount { get; set; }
        [Required] private DateTime DisplayStartTime { get; set; }
        
        private List<Product> products;
        public virtual List<Product> Products
        {
            get => lazyLoader.Load(this, ref products);
            set => products = value;
        }
    }
}