using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("Events")]
    public class Event
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public int AccountId { get; set; }
        [Required] public int EventTypeId { get; set; }
        [Required] public DateTime DateTime { get; set; }
        [ForeignKey("AccountId")] public Account Account { get; set; }
        [ForeignKey("EventTypeId")] public EventType EventType { get; set; }
    }
}