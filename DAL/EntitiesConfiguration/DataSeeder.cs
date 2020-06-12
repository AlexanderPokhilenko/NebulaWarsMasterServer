using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class DataSeeder
    {
        public void TrySeed(ApplicationDbContext dbContext)
        {
            //Если таблица пуста
            if (!dbContext.WarshipTypes.Any())
            {
                var warshipTypes = new List<WarshipType>
                {
                    new WarshipType
                    {
                        Name = "hare",
                        Description = "The hare attacks the enemies with four cannons. It is great for suppression fire. His ability is a shot with a huge charge of plasma.",
                        WarshipCombatRole = new WarshipCombatRole
                        {
                            Name = "DURABLE"
                        }
                    },
                    new WarshipType
                    {
                        Name = "bird", 
                        Description = "The bird attacks the enemies with plasma charges and a laser. The laser breaks through any obstacles. Bird is the fastest ship.",
                        WarshipCombatRole = new WarshipCombatRole
                        {
                            Name = "DAMAGE DEALER"
                        }
                    }
                };
                dbContext.WarshipTypes.AddRange(warshipTypes);
                dbContext.SaveChanges();
            }
        }
    }
}