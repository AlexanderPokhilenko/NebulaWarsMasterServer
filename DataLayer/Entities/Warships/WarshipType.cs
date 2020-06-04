using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Tables
{
    [Table("warship_types")]
    public class WarshipType
    {
        [Column("id")] [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Column("name")] [Required] public string Name { get; set; }
        [Column("description")] [Required] public string Description { get; set; }
        [Column("warship_combat_role_id")] [Required] public int WarshipCombatRoleId { get; set; }
        
        public WarshipCombatRole WarshipCombatRole { get; set; }

        public IEnumerable<Warship> Warships { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{GetType().Name} ");
            stringBuilder.Append($"{nameof(Id)} {Id} ");
            stringBuilder.Append($"{nameof(Name)} {Name} ");
            stringBuilder.Append($"{nameof(Description)} {Description} ");
            stringBuilder.Append($"{nameof(WarshipCombatRoleId)} {WarshipCombatRoleId} ");
            return stringBuilder.ToString();
        }
    }
}