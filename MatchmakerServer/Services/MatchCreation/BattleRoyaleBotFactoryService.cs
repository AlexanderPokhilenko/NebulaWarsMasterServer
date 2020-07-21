using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    public class BattleRoyaleBotFactoryService
    {
        private readonly List<string> warshipNames;
        private static readonly Random Random = new Random();

        public BattleRoyaleBotFactoryService(ApplicationDbContext dbContext)
        {
            warshipNames = dbContext.WarshipTypes.Select(warshipType => warshipType.Name).ToList();
        }
        
        public List<BotModel> CreateBotModels(int numberOdBots)
        {
            List<BotModel> bots = new List<BotModel>();
            for (int i = 0; i < numberOdBots; i++)
            {
                int randomIndex = Random.Next(warshipNames.Count);
                Console.WriteLine(randomIndex);
                Console.WriteLine(warshipNames.Count);
                string warshipName = warshipNames[randomIndex];
                ushort id = BotTemporaryIdFactory.Create();
                BotModel botModel = new BotModel
                {
                    BotName = GenerateNicknameFromId(id),
                    WarshipName = warshipName,
                    TemporaryId = id,
                    WarshipPowerLevel = 1
                };
                bots.Add(botModel);
            }
            
            return bots;
        }

        private string GenerateNicknameFromId(ushort id)
        {
            // разбиваем 2 байта (ushort) на 2 переменные по 1 байту
            var firstIndex = (byte)id;
            var secondIndex = (byte)(id >> 8 + firstIndex);

            var rnd = Random.Next(3);
            switch (rnd)
            {
                case 0:
                    return _adjectives[firstIndex % _adjectives.Length] + " " + _nouns[secondIndex % _nouns.Length];
                case 1:
                    return _nouns[firstIndex % _nouns.Length] + "-" + _nouns[secondIndex % _nouns.Length];
                case 2:
                    return _nouns[firstIndex % _nouns.Length] + " " + _endings[secondIndex % _endings.Length];
                default:
                    return "ERROR";
            }
        }

        private readonly string[] _adjectives =
        {
            "Great", "Cool", "Severe", "First", "Artificial", "Robotic", "Flying", "Skyborn",
            "Nightmarish", "Horrible", "Gentle", "Grumpy", "Glitchy", "Drunken", "Creepy", "Defective"
        };

        private readonly string[] _nouns =
        {
            "Ace", "Conqueror", "Reaver", "Pirate", "Warrior", "Fighter", "Dominator", "Ninja",
            "Demon", "Mutant", "Homunculus", "Sensei", "Machine", "Bot", "Pickle", "Reaper"
        };

        private readonly string[] _endings =
        {
            "From Hell", "With Gun", "Without Heart", "[Still Alive]", "Dominus", "(But Not Quite)", "2020", "From Graveyard",
            "Fully-Loaded", "Never Gives Up", "From Future", "Powered by Memes", "_Dead Inside_", "With Tentacles", "80 LVL", "From Nowhere"
        };
    }
}