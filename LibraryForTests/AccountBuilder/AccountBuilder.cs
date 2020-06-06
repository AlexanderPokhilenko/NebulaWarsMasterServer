using System;
using DataLayer.Tables;

namespace LibraryForTests
{
    public class AccountBuilder
    {
        private readonly Account account = new Account();
        private readonly Random random;

        public AccountBuilder(int seed=1)
        {
            random = new Random(seed);
        }
        
        public void SetBaseInfo(string username, string serviceId, DateTime registrationTime)
        {
            account.Username = username;
            account.ServiceId = serviceId;
            account.RegistrationDateTime = registrationTime;
        }
        
        public void AddWarship(int numberOfMatches)
        {
            Warship warship = new Warship()
            {
                PowerLevel = random.Next(100),
                PowerPoints = random.Next(100),
                WarshipTypeId = account.Warships.Count+1
            };
                
            //Добавить мачти для корабля
            for (int j = 0; j < numberOfMatches; j++)
            {
                DateTime start = new DateTime(2020, 1, 1).AddDays(random.Next(100));
                bool isFinished = random.Next() % 2 == 0;

                MatchResultForPlayer matchResultForPlayer;
                if (isFinished)
                {
                    matchResultForPlayer = new MatchResultForPlayer()
                    {    
                        IsFinished = true,
                        WasShown = random.Next()%2 == 0,
                        PlaceInMatch = random.Next(100),
                        SoftCurrencyDelta = random.Next(20),
                        BigLootboxPoints = random.Next(20),
                        SmallLootboxPoints = random.Next(20),
                        WarshipRatingDelta = random.Next(10),
                        Match = new Match
                        {
                            StartTime = start,
                            FinishTime = start.AddSeconds(random.Next(100)),
                            GameServerIp = "5",
                            GameServerUdpPort = 5
                        }
                    };
                }
                else
                {
                    matchResultForPlayer = new MatchResultForPlayer()
                    {    
                        IsFinished = false,
                        WasShown = false,
                        PlaceInMatch = 0,
                        SoftCurrencyDelta = 0,
                        BigLootboxPoints = 0,
                        SmallLootboxPoints = 0,
                        WarshipRatingDelta = 0,
                        Match = new Match
                        {
                            StartTime = start,
                            FinishTime = null,
                            GameServerIp = "5",
                            GameServerUdpPort = 5
                        }
                    };
                }
                
                warship.MatchResultForPlayers.Add(matchResultForPlayer);
            }
            account.Warships.Add(warship);
        }

        public void AddLootbox(int countOfRegularCurrencyPrizes, int countOfWarshipPowerPointsPrizes, 
            int countOfPointsForSmallLootboxPrizes, bool wasShown, LootboxType lootboxType = LootboxType.Small)
        {
            LootboxDb lootboxDb = new LootboxDb
            {
                CreationDate = DateTime.Now,
                WasShown = wasShown,
                LootboxType = lootboxType
            };

            for (int i = 0; i < countOfRegularCurrencyPrizes; i++)
            {
                var prize = new LootboxPrizeSoftCurrency
                {
                    Quantity = random.Next(30)
                };
                lootboxDb.LootboxPrizeSoftCurrency.Add(prize);
            }
            
            for (int i = 0; i < countOfWarshipPowerPointsPrizes; i++)
            {
                int warshipIndex = random.Next(account.Warships.Count);
                var prize = new LootboxPrizeWarshipPowerPoints
                {
                    Quantity = random.Next(30),
                    WarshipId = account.Warships[warshipIndex].Id
                };
                
                lootboxDb.LootboxPrizeWarshipPowerPoints.Add(prize);
            }
            
            for (int i = 0; i < countOfPointsForSmallLootboxPrizes; i++)
            {
                var prize = new LootboxPrizeSmallLootboxPoints
                {
                    Quantity = random.Next(30)
                };
                lootboxDb.LootboxPrizePointsForSmallLootboxes.Add(prize);
            }
            
            account.Lootboxes.Add(lootboxDb);
        }

        public Account GetResult()
        {
            return account;
        }
    }
}