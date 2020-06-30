using System;
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
            if (!dbContext.GameModeTypes.Any())
            {
                var gameModes = new List<GameModeType>
                {
                    new GameModeType
                    {
                        Name = GameModeEnum.BattleRoyale.ToString(),
                        Id = GameModeEnum.BattleRoyale
                    }
                };
                dbContext.GameModeTypes.AddRange(gameModes);
                dbContext.SaveChanges();
            }
            
            if (dbContext.GameModeTypes.Count() != Enum.GetNames(typeof(GameModeEnum)).Length)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}