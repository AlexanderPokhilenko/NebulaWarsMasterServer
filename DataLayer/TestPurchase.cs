using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class TestPurchase
    {
        [Key] public int Id { get; set; }
        [Required] public string Data { get; set; }
        [Required] public DateTime DateTime { get; set; }
    }
}