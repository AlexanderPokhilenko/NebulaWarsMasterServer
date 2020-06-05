using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace DeleteMe1
{
    public class DbWork
    {
        private readonly ApplicationDbContext dbContext;

        public DbWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public void TryInsert()
        {
            Account account = new Account
            {
                Username = "username_" + DateTime.Now.ToLongTimeString(),
                ServiceId = "serviceId_" + DateTime.Now.ToLongTimeString(),
                CreationDate = DateTime.Now
            };
            
            account.Warships = new List<Warship>
            {
                new Warship
                {
                    WarshipType = new WarshipType
                    {
                        Description  = "WarshipType Description 1",
                        Name = "WarshipType Name 1",
                        WarshipCombatRole = new WarshipCombatRole
                        {
                            Name = "WarshipCombatRole Name 1"
                        }
                    },
                    PowerLevel = 222,
                    PowerPoints = 333,
                    MatchResultForPlayers = new List<MatchResultForPlayer>()
                    {
                        new MatchResultForPlayer()
                        {
                            WasShown = true,
                            PlaceInMatch = 54,
                            PointsForBigLootbox = 54,
                            PremiumCurrencyDelta = 11,
                            WarshipRatingDelta = 98,
                            PointsForSmallLootbox = 21,
                            RegularCurrencyDelta = 57,
                            Match = new Match()
                            {
                                FinishTime = DateTime.Now,
                                StartTime = DateTime.Today,
                                GameServerIp = "GameServerIp",
                                GameServerUdpPort = 651
                            }
                        }
                    }
                },
                new Warship
                {
                    WarshipType = new WarshipType
                    {
                        Description  = "WarshipType Description 2",
                        Name = "WarshipType Name 2",
                        WarshipCombatRole = new WarshipCombatRole
                        {
                            Name = "WarshipCombatRole Name 2"
                        }
                    },
                    PowerLevel = 99,
                    PowerPoints = 999,
                    MatchResultForPlayers = new List<MatchResultForPlayer>()
                    {
                        new MatchResultForPlayer()
                        {
                            WasShown = false,
                            PlaceInMatch = 999,
                            PointsForBigLootbox = 5999,
                            PremiumCurrencyDelta = 9,
                            WarshipRatingDelta = 999,
                            PointsForSmallLootbox = 2991,
                            RegularCurrencyDelta = 9957,
                            Match = new Match()
                            {
                                FinishTime = DateTime.Now,
                                StartTime = DateTime.Today,
                                GameServerIp = "работай сич2",
                                GameServerUdpPort = 651
                            }
                        }
                    }
                }
            };

              
            account.Lootboxes = new List<LootboxDb>
            {
                new LootboxDb
                {
                    CreationDate = DateTime.Now,
                    LootboxType = LootboxType.Big,
                    WasShown = false,
                    LootboxPrizeRegularCurrencies = new List<LootboxPrizeRegularCurrency>()
                    {
                        new LootboxPrizeRegularCurrency()
                        {
                            Quantity = 150
                        },
                        new LootboxPrizeRegularCurrency()
                        {
                            Quantity = 1506
                        },
                        new LootboxPrizeRegularCurrency()
                        {
                            Quantity = 888
                        },
                    },
                    LootboxPrizeWarshipPowerPoints = new List<LootboxPrizeWarshipPowerPoints>
                    {
                        new LootboxPrizeWarshipPowerPoints()
                        {
                            Quantity = 99,
                            WarshipId = account.Warships.First().Id
                        },new LootboxPrizeWarshipPowerPoints()
                        {
                            Quantity = 9956,
                            WarshipId = account.Warships.First().Id
                        },new LootboxPrizeWarshipPowerPoints()
                        {
                            Quantity = 9149,
                            WarshipId = account.Warships.First().Id
                        },new LootboxPrizeWarshipPowerPoints()
                        {
                            Quantity = 9149,
                            WarshipId = account.Warships.Last().Id
                        }
                    },
                    LootboxPrizePointsForSmallLootboxes = new List<LootboxPrizePointsForSmallLootbox>
                    {
                        new LootboxPrizePointsForSmallLootbox()
                        {
                            Quantity = 981
                        },
                        new LootboxPrizePointsForSmallLootbox()
                        {
                            Quantity = 111
                        },
                        new LootboxPrizePointsForSmallLootbox()
                        {
                            Quantity = 116163
                        }
                    }
                }
            };
          
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
        }

        public string GetSomeServiceId()
        {
            return dbContext.Accounts.FirstOrDefault()?.ServiceId;
        }
    }
}