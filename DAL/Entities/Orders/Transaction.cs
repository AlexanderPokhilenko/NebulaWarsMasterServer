using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    /// <summary>
    /// Заказ может содержать несколько ресурсов (комплект).
    /// </summary>
    public class Transaction
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public TransactionTypeEnum TransactionTypeId { get; set; }
        [Required] public DateTime DateTime { get; set; }
        [Required] public bool WasShown { get; set; }
        public Account Account { get; set; }
        public TransactionType TransactionType { get; set; }
        public List<Resource> Resources { get; set; }
    }
}