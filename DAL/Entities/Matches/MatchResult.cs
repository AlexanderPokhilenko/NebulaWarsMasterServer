using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Tables
{
    public class MatchResult
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        
        [Required] public int MatchId { get; set; }
        [Required] public int WarshipId { get; set; }
        public int? TransactionId { get; set; }
        
        [Required] public bool IsFinished { get; set; }
        [Required] public int PlaceInMatch { get; set; }
        
        public Match Match { get; set; }
        public Warship Warship { get; set; }
        public Transaction Transaction { get; set; }
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{GetType().Name} ");
            stringBuilder.Append($"{nameof(Id)} {Id} ");
            stringBuilder.Append($"{nameof(MatchId)} {MatchId} ");
            stringBuilder.Append($"{nameof(WarshipId)} {WarshipId} ");
            stringBuilder.Append($"{nameof(PlaceInMatch)} {PlaceInMatch} ");
            return stringBuilder.ToString();
        }
    }
}