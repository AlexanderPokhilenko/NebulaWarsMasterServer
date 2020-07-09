using System.Collections.Generic;

namespace DataLayer.Tables
{
    public class WarshipDbDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int WarshipTypeId { get; set; }
        public int WarshipPowerLevel { get; set; }
        public int WarshipPowerPoints { get; set; }
        public int WarshipRating { get; set; }
        public AccountDbDto Account { get; set; }
        public WarshipType WarshipType { get; set; }
        public List<string> Skins { get; set; } = new List<string>();
    }
}