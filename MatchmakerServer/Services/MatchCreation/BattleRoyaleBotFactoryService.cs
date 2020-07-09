using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services.Queues;
using AmoebaGameMatcherServer.Utils;
using DataLayer;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    public class BattleRoyaleBotFactoryService
    {
        private readonly List<string> warshipNames;
        private static readonly Random random = new Random();

        public BattleRoyaleBotFactoryService(ApplicationDbContext dbContext)
        {
            warshipNames = dbContext.WarshipTypes.Select(warshipType => warshipType.Name).ToList();
        }
        
        public List<BotModel> CreateBotModels(int numberOdBots)
        {
            List<BotModel> bots = new List<BotModel>();
            for (int i = 0; i < numberOdBots; i++)
            {
                string warshipName = warshipNames[random.Next(warshipNames.Count)];
                BotModel botModel = new BotModel
                {
                    BotName = "Bot " + i,
                    WarshipName = warshipName,
                    TemporaryId = BotTemporaryIdFactory.Create(),
                    WarshipPowerLevel = 1
                };
                bots.Add(botModel);
            }
            
            return bots;
        }
    }
}