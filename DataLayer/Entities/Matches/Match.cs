using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    [Table("matches")]
    public class Match
    {
        [Column("id")] [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Column("start_time")] [Required] public DateTime StartTime { get; set; }
        [Column("finish_time")] public DateTime? FinishTime { get; set; }
        [Column("game_server_ip")] [Required] public string GameServerIp { get; set; }
        [Column("game_server_udp_port")] [Required] public int GameServerUdpPort { get; set; }

        public List<MatchResultForPlayer> MatchResultForPlayers { get; set; }
    }
}