using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.Database.Seeding.Seaders
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
            
            if (dbContext.WarshipCombatRoles.Count() != Enum.GetNames(typeof(WarshipCombatRoleEnum)).Length)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}