using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    /// <summary>
    /// Отвечает за транзакцию. Хранит, что было потрачено и что было приобретено.
    /// </summary>
    public class Resource
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int TransactionId { get; set; }
        [Required] public ResourceTypeEnum ResourceTypeId { get; set; }
        public Transaction Transaction { get; set; }
        public ResourceType ResourceType { get; set; }
        public List<Increment> Increments { get; set; }
        public List<Decrement> Decrements { get; set; }
    }
}