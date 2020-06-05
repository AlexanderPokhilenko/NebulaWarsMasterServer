using System;
using System.Collections.Generic;
using DataLayer.Tables;

namespace LibraryForTests
{
    public class TestAccountFactory
    {
        public Account CreateAccount(string username="igor", string serviceId="serviceIdIgor", int numberOfWarships=0,
            int numberOfMatches=0, int seed=1)
        {
            Random random = new Random(seed);
            Account account = new Account
            {
                CreationDate = DateTime.Now,
                Username = username,
                ServiceId = serviceId,
                PremiumCurrency = random.Next(),
                RegularCurrency = random.Next(),
                PointsForSmallLootbox = random.Next()
            };

            //Добавить корабли
            for (int i = 0; i < numberOfWarships; i++)
            {
                Warship warship = new Warship()
                {
                    PowerLevel = random.Next(100),
                    PowerPoints = random.Next(100),
                    WarshipTypeId = i+1,
                    MatchResultForPlayers = new List<MatchResultForPlayer>()
                };
                
                //Добавить мачти для кораблей
                for (int j = 0; j < numberOfMatches; j++)
                {
                    DateTime start = new DateTime(2020, 1, 1).AddDays(random.Next(100));
                    MatchResultForPlayer matchResultForPlayer = new MatchResultForPlayer()
                    {    
                        WasShown = random.Next()%2 == 0,
                        PlaceInMatch = random.Next(100),
                        PremiumCurrencyDelta = random.Next(100),
                        RegularCurrencyDelta = random.Next(100),
                        PointsForBigLootbox = random.Next(100),
                        PointsForSmallLootbox = random.Next(100),
                        WarshipRatingDelta = random.Next(100),
                        Match = new Match
                        {
                            StartTime = start,
                            FinishTime = start.AddSeconds(random.Next(100)),
                            GameServerIp = "5",
                            GameServerUdpPort = 5
                        }
                    };
                    warship.MatchResultForPlayers.Add(matchResultForPlayer);
                }
                account.Warships.Add(warship);
            }

            return account;
        }
    }
}