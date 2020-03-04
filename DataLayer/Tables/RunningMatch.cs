using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("RunningMatches")]
    public class RunningMatch
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public DateTime StartTime { get; set; }
        [Required] public string GameServerIp { get; set; }
        [Required] public string GameServerPorn { get; set; }
    }
}