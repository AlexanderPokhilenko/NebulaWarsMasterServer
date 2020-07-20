using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.Database.Seeding.Seaders
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
                        Description = "Hare attacks enemies with 4 machine guns and medium plasma cannon. It is great for suppressive fire. Its ability is a huge ball of energy, which can ignore obstacles and shields.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.Tank
                    },
                    new WarshipType
                    {
                        Id = WarshipTypeEnum.Bird,
                        Name = "bird", 
                        Description = "Bird attacks enemies with 2 plasma guns and a laser. Its laser breaks through any obstacles. Laser overloading, which causes huge damage, can be used for finishing off enemies.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.DamageDealer
                    },new WarshipType
                    {
                        Id = WarshipTypeEnum.Smiley,
                        Name = "smiley",
                        Description = "Smiley attacks enemies with 8 cannons (4 at the same time). This ship has the most health points. Its spider missiles are great for neutralizing unwary and slow opponents.",
                        WarshipCombatRoleId = WarshipCombatRoleEnum.Tank
                    },new WarshipType
                    {
                        Id = WarshipTypeEnum.Sage,
                        Name = "sage",
                        Description = "Sage is the fastest warship, but has lowest health points. Its interceptors deal damage comparable to blasters. Fast ability recharge allows you to have two interceptors at the same time.",
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