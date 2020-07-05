using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Tables;

namespace LibraryForTests
{
    /// <summary>
    /// Содержит методы для поэтапного создания аккаунта.
    /// Нужно для тестов
    /// </summary>
    public class AccountBuilder
    {
        private readonly Account account = new Account();
        private readonly Random random;

        public AccountBuilder(int seed = 1)
        {
            random = new Random(seed);
        }
        
        public void SetBaseInfo(string username, string serviceId, DateTime registrationTime)
        {
            account.Username = username;
            account.ServiceId = serviceId;
            account.RegistrationDateTime = registrationTime;
        }
        
        public void AddWarship()
        {
            Warship warship = new Warship
            {
                WarshipTypeId = (WarshipTypeEnum) account.Warships.Count+1
            };
            
            account.Warships.Add(warship);
        }
        
        public void AddWarshipImprovements(int numberOfImprovements)
        {
            for (int index = 0; index < numberOfImprovements; index++)
            {
                int warshipIndex = random.Next(account.Warships.Count);
                int warshipId = account.Warships[warshipIndex].Id;
                int amount = random.Next(33);
                Increment increment = new Increment()
                {
                    IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                    Amount = amount,
                    WarshipId = warshipId
                };
              
                Transaction transaction = new Transaction()
                {
                    AccountId = account.Id,
                    DateTime = DateTime.Now,
                    TransactionTypeId = TransactionTypeEnum.Lootbox,
                    WasShown = false,
                    Increments = new List<Increment>()
                    {
                        increment
                    },
                };
                account.Transactions.Add(transaction);
            }
        }
        
        /// <summary>
        /// Нельзя вызывать, если в БД не были сохранены корабли. В таком случает бросит исключение
        /// из-за проблем с внешним ключём на сущность Warship
        /// </summary>
        public void AddMatches(int numberOfMatches)
        {
            for (int warshipIndex = 0; warshipIndex < account.Warships.Count; warshipIndex++)
            {
                Warship warship = account.Warships[warshipIndex];
                for (int j = 0; j < numberOfMatches; j++)
                {
                    MatchResult matchResultForPlayer;
                    DateTime start = new DateTime(2020, 1, 1).AddDays(random.Next(100));
                    bool isFinished = random.Next() % 2 == 0;
                    if (isFinished)
                    {
                        bool wasShown = random.Next() % 2 == 0;
                        matchResultForPlayer = new MatchResult()
                        {    
                            IsFinished = true,
                            PlaceInMatch = random.Next(30),
                            Match = new Match
                            {
                                StartTime = start,
                                FinishTime = start.AddSeconds(random.Next(100)),
                                GameServerIp = "5",
                                GameServerUdpPort = 5,
                                GameModeId = GameModeEnum.BattleRoyale
                            },
                            Transaction = new Transaction()
                            {
                                DateTime = DateTime.Now,
                                TransactionTypeId = TransactionTypeEnum.MatchReward,
                                WasShown = wasShown,
                                Increments = new List<Increment>()
                                {
                                    new Increment()
                                    {
                                        Amount = random.Next(10),
                                        WarshipId = warship.Id,
                                        IncrementTypeId = IncrementTypeEnum.WarshipRating 
                                    },
                                    new Increment()
                                    {
                                        Amount = random.Next(10),
                                        IncrementTypeId = IncrementTypeEnum.LootboxPoints
                                    }
                                },
                                AccountId = account.Id
                            }
                        };
                    }
                    else
                    {
                        matchResultForPlayer = new MatchResult()
                        {    
                            IsFinished = false,
                            PlaceInMatch = 0,
                            Match = new Match
                            {
                                StartTime = start,
                                FinishTime = null,
                                GameServerIp = "5",
                                GameServerUdpPort = 5,
                                GameModeId = GameModeEnum.BattleRoyale
                            }
                        };
                    }
                    
                    warship.MatchResults.Add(matchResultForPlayer);
                }
            }
        }

        /// <summary>
        /// Нельзя вызывать, если в БД не были сохранены корабли. В таком случает бросит исключение
        /// из-за проблем с внешним ключём на сущность Warship
        /// </summary>
        public void AddLootbox(int countOfSoftCurrencyPrizes, int countOfWarshipPowerPointsPrizes, 
            int countOfLootboxPointsPrizes, bool wasShown)
        {
            var increments = new List<Increment>();
            Transaction transaction = new Transaction()
            {
                DateTime = DateTime.Now,
                TransactionTypeId = TransactionTypeEnum.Lootbox,
                WasShown = wasShown,
                Increments = increments,
                AccountId = account.Id
            };

            for (int i = 0; i < countOfSoftCurrencyPrizes; i++)
            {
                Increment increment = new Increment()
                {
                    Amount = random.Next(30),
                    IncrementTypeId = IncrementTypeEnum.SoftCurrency
                };
                increments.Add(increment);
            }
            
            for (int i = 0; i < countOfWarshipPowerPointsPrizes; i++)
            {
                int warshipIndex = random.Next(account.Warships.Count);
                int warshipId = account.Warships[warshipIndex].Id;
                Increment increment = new Increment
                {
                    Amount = random.Next(30),
                    WarshipId = warshipId,
                    IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints
                };
                
                increments.Add(increment);
            }
            
            for (int i = 0; i < countOfLootboxPointsPrizes; i++)
            {
                Increment increment = new Increment()
                {
                    Amount = random.Next(30),
                    IncrementTypeId = IncrementTypeEnum.LootboxPoints
                };
                increments.Add(increment);
            }
            
            account.Transactions.Add(transaction);
        }

        public Account GetAccount()
        {
            return account;
        }

        public void AddWarshipLevels(int maxLevel=15)
        {
            for (int warshipIndex = 0; warshipIndex < account.Warships.Count; warshipIndex++)
            {
                Warship warship = account.Warships[warshipIndex];
                int warshipId = warship.Id;
                int level = random.Next(1, maxLevel);
                for (int i = 1; i <= level; i++)
                {
                    Transaction transaction = new Transaction()
                    {
                        AccountId = account.Id,
                        DateTime = DateTime.UtcNow,
                        TransactionTypeId = TransactionTypeEnum.DailyPrize,
                        WasShown = false,
                        Increments = new List<Increment>()
                        {
                            new Increment()
                            {
                                IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                                WarshipId = warshipId,
                                Amount = i
                            }
                        }
                    };
                    account.Transactions.Add(transaction);
                }
            }
        }
    }
}