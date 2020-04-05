using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLayer.Tables
{
    [Table("Matches")]
    public class Match
    {
        private readonly ILazyLoader lazyLoader;
        public Match()
        {
        }
        public Match(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
        [Required] public string GameServerIp { get; set; }
        [Required] public int GameServerUdpPort { get; set; }

        private List<MatchResultForPlayer> matchResultForPlayers;
        public virtual List<MatchResultForPlayer> MatchResultForPlayers
        {
            get => lazyLoader.Load(this, ref matchResultForPlayers);
            set => matchResultForPlayers = value;
        }
    }
}