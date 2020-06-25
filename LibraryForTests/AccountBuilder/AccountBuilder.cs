using System;
using System.Collections.Generic;
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
                                Resources = new List<Resource>()
                                {
                                    new Resource()
                                    {
                                        Increments = new List<Increment>()
                                        {
                                            new Increment()
                                            {
                                                LootboxPoints = random.Next(10),
                                                WarshipRating = random.Next(10),
                                                WarshipId = warship.Id,
                                                IncrementTypeId = IncrementTypeEnum.MatchReward 
                                            }
                                        },
                                        ResourceTypeId =ResourceTypeEnum.MatchReward 
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
                WasShown = random.Next() % 2 == 0,
                Resources = new List<Resource>()
                {
                    new Resource()
                    {
                        Increments = increments,
                        ResourceTypeId = ResourceTypeEnum.Lootbox
                    }
                },
                AccountId = account.Id
            };

            for (int i = 0; i < countOfSoftCurrencyPrizes; i++)
            {
                Increment increment = new Increment()
                {
                    SoftCurrency = random.Next(30),
                    IncrementTypeId = IncrementTypeEnum.Currency
                };
                increments.Add(increment);
            }
            
            for (int i = 0; i < countOfWarshipPowerPointsPrizes; i++)
            {
                int warshipId = random.Next(account.Warships.Count);
                Increment increment = new Increment()
                {
                    WarshipPowerPoints = random.Next(30),
                    WarshipId = warshipId,
                    IncrementTypeId = IncrementTypeEnum.Currency
                };
                
                increments.Add(increment);
            }
            
            for (int i = 0; i < countOfLootboxPointsPrizes; i++)
            {
                Increment increment = new Increment()
                {
                    LootboxPoints = random.Next(30),
                    IncrementTypeId = IncrementTypeEnum.Lootbox
                };
                increments.Add(increment);
            }
            
            account.Transactions.Add(transaction);
        }

        public Account GetAccount()
        {
            return account;
        }
    }
}