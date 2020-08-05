using System;
using System.Collections.Generic;
using System.Linq;
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
            if (warshipNames.Count == 0)
            {
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Контекст не содержит названия кораблей");
                Console.WriteLine("Error. Создание нового контекста");
                ApplicationDbContext sukaContext = new DbContextFactory().Create(); 
                warshipNames = sukaContext.WarshipTypes.Select(warshipType => warshipType.Name).ToList();
            }
            
            
            if (warshipNames.Count == 0)
            {
                warshipNames = new List<string>()
                {
                    "hare",
                    "bird",
                    "smiley",
                    "sage"
                };
            }
            
            if (warshipNames.Count == 0)
            {
                throw new Exception("Список типов кораблей пуст после всех костылей");
            }
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
                    BotName = GenerateNickname(id, warshipName),
                    WarshipName = warshipName,
                    TemporaryId = id,
                    WarshipPowerLevel = 1
                };
                bots.Add(botModel);
            }
            
            return bots;
        }

        private string GenerateNickname(ushort id, string warshipName)
        {
            // разбиваем 2 байта (ushort) на 2 переменные по 1 байту
            var firstIndex = (byte)id;
            var secondIndex = (byte)((id >> 8) + firstIndex);

            var rnd = Random.Next(5);
            switch (rnd)
            {
                case 0:
                    return _adjectives[firstIndex % _adjectives.Length] + " " + _nouns[secondIndex % _nouns.Length];
                case 1:
                    if (firstIndex == secondIndex) return _adjectives[firstIndex % _adjectives.Length] + " " + char.ToUpper(warshipName[0]) + warshipName.Substring(1);
                    return _nouns[firstIndex % _nouns.Length] + "-" + _nouns[secondIndex % _nouns.Length];
                case 2:
                    return _nouns[firstIndex % _nouns.Length] + " From " + _fromParts[secondIndex % _fromParts.Length];
                case 3:
                    return _adjectives[firstIndex % _adjectives.Length] + " " + _names[secondIndex % _names.Length];
                case 4:
                    return _names[firstIndex % _names.Length] + " The " + _nouns[secondIndex % _nouns.Length];
                default:
                    return "ERROR";
            }
        }

        private readonly string[] _adjectives =
        {
            "Great", "Cool", "Supreme", "Mighty", "Artificial", "Robotic", "Space", "Skyborn",
            "Nightmarish", "Horrible", "Gentle", "Grumpy", "Glitchy", "Powerful", "Creepy", "Defective"
        };

        private readonly string[] _nouns =
        {
            "Ace", "Conqueror", "Reaver", "Pirate", "Warrior", "Fighter", "Goblin", "Ninja",
            "Demon", "Mutant", "Elite", "Samurai", "Machine", "Bot", "Monster", "Reaper"
        };

        private readonly string[] _fromParts =
        {
            "Hell", "Space", "Graveyard", "Future", "Nowhere", "Nightmare"
        };

        private readonly string[] _names =
        {
            "John", "Mary", "Jack", "Bill", "Jane", "Ivan", "Peter", "Taras",
            "Benedict", "Suzanne", "Ulrich", "Vladimir", "Rick", "Timur", "Anton", "Alexis",
            "James", "Jimmy", "Gandalf", "Timmy", "Arnold", "Harry", "Barbossa", "Amber"
        };
    }
}