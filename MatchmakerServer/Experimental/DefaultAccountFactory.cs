using System;
using System.Collections.Generic;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services
{
    public static class DefaultAccountFactory
    {
        private static readonly int defaultPremiumCurrency = 0;
        private static readonly int defaultRegularCurrency = 150;
        private static readonly int defaultPointsForBigChest = 100;
        private static readonly int DefaultPointsForSmallChest = 100;
        private static readonly int defaultWarshipCombatPowerLevel = 0;
        private static readonly int defaultWarshipCombatPowerValue = 0;
        private static readonly int warshipTypeHare = 1;
        private static readonly int warshipTypeBird = 2;

        public static Account CreateDefaultAccount(string playerId)
        {
            Account account = new Account
            {
                ServiceId = playerId,
                Username = playerId,
                PremiumCurrency = defaultPremiumCurrency,
                CreationDate = DateTime.UtcNow,
                RegularCurrency = defaultRegularCurrency,
                PointsForBigLootbox = defaultPointsForBigChest,
                PointsForSmallLootbox = DefaultPointsForSmallChest,
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        WarshipTypeId = warshipTypeHare,
                        CombatPowerLevel = defaultWarshipCombatPowerLevel,
                        CombatPowerValue = defaultWarshipCombatPowerValue
                    },
                    new Warship
                    {
                        WarshipTypeId = warshipTypeBird,
                        CombatPowerLevel = defaultWarshipCombatPowerLevel,
                        CombatPowerValue = defaultWarshipCombatPowerValue
                    }
                }
            };
            return account;
        }
    }
}