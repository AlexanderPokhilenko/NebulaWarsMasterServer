using System;
using System.Collections.Generic;

namespace DataLayer.Tables
{
    public class AccountDbDto
    {
        public int Id { get; set; }
        public string ServiceId { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public int SoftCurrency { get; set; }
        public int HardCurrency { get; set; }
        public int LootboxPoints { get; set; }
        public int Rating { get; set; }
        
        public List<WarshipDbDto> Warships { get; set; } = new List<WarshipDbDto>();
    }
}