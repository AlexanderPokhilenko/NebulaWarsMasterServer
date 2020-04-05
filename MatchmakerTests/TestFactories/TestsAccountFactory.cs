using System;
using System.Collections.Generic;
using DataLayer.Tables;

namespace MatchmakerTest.Utils
{
    public static class TestsAccountFactory
    {
        /// <summary>
        /// Создаёт аккаунт с уникальным serviceId, кораблём, двумя законченными боями и одним не законченным.
        /// </summary>
        /// <returns></returns>
        public static Account CreateUniqueAccount()
        {
            return new Account
            {
                ServiceId = UniqueStringFactory.Create(),
                Username = "Игорь",
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        WarshipTypeId = 1,
                        MatchResultForPlayers = new List<MatchResultForPlayer>
                        {
                            new MatchResultForPlayer
                            {
                                Match = new Match
                                {
                                    StartTime = DateTime.Now,
                                    FinishTime = DateTime.Now + TimeSpan.FromMinutes(5),
                                    GameServerIp = "someIp",
                                    GameServerUdpPort = 668
                                },
                                PlaceInMatch = 2,
                                PremiumCurrencyDelta = 4,
                                RegularCurrencyDelta = 34,
                                PointsForBigChest = 0,
                                PointsForSmallChest = 5,
                                JsonMatchResultDetails = null,
                                WarshipRatingDelta = 9
                            },
                            new MatchResultForPlayer
                            {
                                Match = new Match
                                {
                                    StartTime = DateTime.Now,
                                    FinishTime = DateTime.Now + TimeSpan.FromMinutes(5),
                                    GameServerIp = "someIp",
                                    GameServerUdpPort = 668
                                },
                                PlaceInMatch = 4,
                                PremiumCurrencyDelta = 0,
                                RegularCurrencyDelta = 12,
                                PointsForBigChest = 1,
                                PointsForSmallChest = 2,
                                JsonMatchResultDetails = null,
                                WarshipRatingDelta = 3
                            },
                            new MatchResultForPlayer
                            {
                                Match = new Match
                                {
                                    StartTime = DateTime.Now,
                                    FinishTime = null,
                                    GameServerIp = "someIp",
                                    GameServerUdpPort = 668
                                },
                                PlaceInMatch = null,
                                PremiumCurrencyDelta = null,
                                RegularCurrencyDelta = null,
                                PointsForBigChest = null,
                                PointsForSmallChest = null,
                                JsonMatchResultDetails = null,
                                WarshipRatingDelta = null
                            },
                        }
                    }
                }
            };
        }

        public static List<Account> CreateAccounts(int count)
        {
            List<Account> result = new List<Account>();
            for (int i = 0; i < count; i++)
            {
                var account = CreateUniqueAccount();
                result.Add(account);
            }

            return result;
        }
    }
}