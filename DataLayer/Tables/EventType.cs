using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Tables
{
    [Table("EventTypes")]
    public class EventType
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] public string Name { get; set; }
    }

    public enum EventTypeEnum:byte
    {
        GameEntry = 0,
        GameExit = 1,
        StartButtonClicked = 2,
        BattleExitButtonClicked = 3
    }
}