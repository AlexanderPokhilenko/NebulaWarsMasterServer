using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace DataLayer.Tables
{
    public class WarshipDbDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public WarshipTypeEnum WarshipTypeId { get; set; }
        public int WarshipPowerLevel { get; set; }
        public int WarshipPowerPoints { get; set; }
        public int WarshipRating { get; set; }
        public AccountDbDto Account { get; set; }
        public WarshipType WarshipType { get; set; }
        public List<SkinType> Skins { get; set; } = new List<SkinType>();
        public SkinType CurrentSkinType { get; set; }
        public SkinTypeEnum? CurrentSkinTypeId { get; set; }
    }
}