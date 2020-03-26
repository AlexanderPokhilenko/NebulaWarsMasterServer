using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("Matches")]
    public class Match
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
        [Required] public string GameServerIp { get; set; }
        [Required] public int GameServerUdpPort { get; set; }
        
        public List<PlayerMatchResult> PlayerMatchResults { get; set; }
    }
}