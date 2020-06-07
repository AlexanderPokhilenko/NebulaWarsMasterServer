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
        private static readonly int defaultPointsForSmallChest = 100;
        private static readonly int defaultWarshipCombatPowerLevel = 1;
        private static readonly int defaultWarshipCombatPowerValue = 0;
        private static readonly int warshipTypeHare = 1;
        private static readonly int warshipTypeBird = 2;
        private static readonly int warshipTypeSmiley = 3;

        public static Account CreateDefaultAccount(string playerId)
        {
            Account account = new Account
            {
                ServiceId = playerId,
                Username = playerId,
                PremiumCurrency = defaultPremiumCurrency,
                CreationDate = DateTime.UtcNow,
                RegularCurrency = defaultRegularCurrency,
                PointsForSmallLootbox = defaultPointsForSmallChest,
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        WarshipTypeId = warshipTypeHare,
                        PowerLevel = defaultWarshipCombatPowerLevel
                    },
                    new Warship
                    {
                        WarshipTypeId = warshipTypeBird,
                        PowerLevel = defaultWarshipCombatPowerLevel
                    },
                    new Warship
                    {
                        WarshipTypeId = warshipTypeSmiley,
                        PowerLevel = defaultWarshipCombatPowerLevel
                    }
                }
            };
            
            return account;
        }
    }
}