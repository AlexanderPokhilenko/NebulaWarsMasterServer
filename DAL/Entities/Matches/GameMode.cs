using System.ComponentModel.DataAnnotations;

namespace DataLayer.Tables
{
    public class GameMode
    {
        [Key] public GameModeEnum Id { get; set; }
        [Required] public string Name { get; set; }
    }
}