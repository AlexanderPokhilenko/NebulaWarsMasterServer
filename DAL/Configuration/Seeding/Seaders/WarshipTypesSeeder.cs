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
                        Description = "The hare attacks the enemies with four cannons. It is great for suppression fire. His ability is a shot with a huge charge of plasma.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.Tank
                    },
                    new WarshipType
                    {
                        Id = WarshipTypeEnum.Bird,
                        Name = "bird", 
                        Description = "The bird attacks the enemies with plasma charges and a laser. The laser breaks through any obstacles. Bird is the fastest ship.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.DamageDealer
                    },new WarshipType
                    {
                        Id = WarshipTypeEnum.Smiley,
                        Name = "smiley",
                        Description = "The smiley attacks the enemies with eight cannons. This ship has more HP, than hare and bird, but slower.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.Tank
                    }
                };
                
                dbContext.WarshipTypes.AddRange(warshipTypes);
                dbContext.SaveChanges();
            }
        }
    }
}