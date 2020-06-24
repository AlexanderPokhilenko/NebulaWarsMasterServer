using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class WarshipCombatRoleSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.WarshipCombatRoles.Any())
            {
                var warshipCombatRoles = new List<WarshipCombatRole>
                {
                    new WarshipCombatRole
                    {
                        Id = WarshipCombatRoleEnum.DamageDealer,
                        Name = "DAMAGE DEALER"
                    },
                    new WarshipCombatRole
                    {
                        Id = WarshipCombatRoleEnum.Tank,
                        Name = "TANK"
                    }
                };
                
                dbContext.WarshipCombatRoles.AddRange(warshipCombatRoles);
                dbContext.SaveChanges();
            }
        }
    }
}