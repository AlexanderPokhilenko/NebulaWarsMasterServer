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
                Rating = 111,
                Username = "username_" + DateTime.Now.ToLongTimeString(),
                ServiceId = "serviceId_" + DateTime.Now.ToLongTimeString(),
                PremiumCurrency = 22222,
                RegularCurrency = 33333,
                CreationDate = DateTime.Now,
                PointsForSmallLootbox = 44444,
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        WarshipType = new WarshipType
                        {
                            Description  = "WarshipType desc 1",
                            Name = "WarshipType name 1",
                            WarshipCombatRole = new WarshipCombatRole
                            {
                                Name = "WarshipCombatRole 1"
                            }
                        },
                        Rating = 1111,
                        PowerLevel = 222,
                        PowerPoints = 333,
                        MatchResultForPlayers = new List<MatchResultForPlayer>()
                        {
                            new MatchResultForPlayer()
                            {
                                WasShown = true,
                                PlaceInMatch = 54,
                                PointsForBigChest = 54,
                                PremiumCurrencyDelta = 11,
                                WarshipRatingDelta = 98,
                                PointsForSmallChest = 21,
                                RegularCurrencyDelta = 57,
                                Match = new Match()
                                {
                                    FinishTime = DateTime.Now,
                                    StartTime = DateTime.Today,
                                    GameServerIp = "работай скотиняка",
                                    GameServerUdpPort = 651
                                }
                            }
                        }
                    },
                    new Warship
                    {
                        WarshipType = new WarshipType
                        {
                            Description  = "сич2",
                            Name = "сич2",
                            WarshipCombatRole = new WarshipCombatRole
                            {
                                Name = "сич 2"
                            }
                        },
                        Rating = 99,
                        PowerLevel = 99,
                        PowerPoints = 999,
                        MatchResultForPlayers = new List<MatchResultForPlayer>()
                        {
                            new MatchResultForPlayer()
                            {
                                WasShown = false,
                                PlaceInMatch = 999,
                                PointsForBigChest = 5999,
                                PremiumCurrencyDelta = 9,
                                WarshipRatingDelta = 999,
                                PointsForSmallChest = 2991,
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