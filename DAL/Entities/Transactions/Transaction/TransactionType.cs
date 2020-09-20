using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    public class TransactionType
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public TransactionTypeEnum Id { get; set; }
        [Required] public string Name { get; set; }
        public List<Transaction> Orders { get; set; }
    }
}