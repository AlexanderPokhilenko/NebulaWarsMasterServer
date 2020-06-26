using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.NetworkLibrary.Experimental;

namespace DataLayer.Tables
{
    public class MatchRewardType
    {
        [Key] public MatchRewardTypeEnum Id { get; set; }
        [Required] public string Name { get; set; }
        public List<Increment> Increments { get; set; }
    }
}