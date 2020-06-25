using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class GameModeSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.GameModes.Any())
            {
                var gameModes = new List<GameMode>
                {
                    new GameMode
                    {
                        Name = GameModeEnum.BattleRoyale.ToString(),
                        Id = GameModeEnum.BattleRoyale
                    }
                };
                dbContext.GameModes.AddRange(gameModes);
                dbContext.SaveChanges();
            }
        }
    }
}