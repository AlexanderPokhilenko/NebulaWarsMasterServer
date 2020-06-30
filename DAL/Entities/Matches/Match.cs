using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    public class Match
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
        [Required] public string GameServerIp { get; set; }
        [Required] public int GameServerUdpPort { get; set; }
        [Required] public GameModeEnum GameModeId { get; set; }
        public GameModeType GameModeType { get; set; }
        public List<MatchResult> MatchResults { get; set; }
    }
}