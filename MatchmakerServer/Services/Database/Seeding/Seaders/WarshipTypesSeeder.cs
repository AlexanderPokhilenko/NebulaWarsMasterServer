using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class WarshipTypesSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.WarshipTypes.Any())
            {
                var warshipTypes = new List<WarshipType>
                {
                    new WarshipType
                    {
                        Id = WarshipTypeEnum.Hare,
                        Name = "hare",
                        Description = "Hare attacks enemies with 4 Gatling guns and medium plasma cannon. It is great for suppression fire. His ability is a shot with a huge charge of plasma.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.Tank
                    },
                    new WarshipType
                    {
                        Id = WarshipTypeEnum.Bird,
                        Name = "bird", 
                        Description = "Bird attacks enemies with 2 plasmaguns and a laser. The laser breaks through any obstacles.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.DamageDealer
                    },new WarshipType
                    {
                        Id = WarshipTypeEnum.Smiley,
                        Name = "smiley",
                        Description = "Smiley attacks enemies with 8 cannons (4 at the same time). This ship has more HP, than hare and bird, but slower.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.Tank
                    },new WarshipType
                    {
                        Id = WarshipTypeEnum.Sage,
                        Name = "sage",
                        Description = "Sage attacks enemies with 2 blasters. This ship has least of all HP, but it is the fastest warship. It can summon interceptors for help.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.DamageDealer
                    }
                };
                
                dbContext.WarshipTypes.AddRange(warshipTypes);
                dbContext.SaveChanges();
            }
            
            if (dbContext.WarshipTypes.Count() != Enum.GetNames(typeof(WarshipTypeEnum)).Length)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}