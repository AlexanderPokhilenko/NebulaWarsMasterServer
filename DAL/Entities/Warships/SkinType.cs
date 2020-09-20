using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace DataLayer.Tables
{
   
    public class SkinType
    {
        [Key] public SkinTypeEnum Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public WarshipTypeEnum WarshipTypeId { get; set; }
        public WarshipType WarshipType { get; set; }
        public List<Increment> Increments { get; set; }
    }
}