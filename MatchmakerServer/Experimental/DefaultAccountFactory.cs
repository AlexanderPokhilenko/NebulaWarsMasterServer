using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Experimental
{
    /// <summary>
    /// Создаёт пустой аккаунт с кораблями по нику пользователя.
    /// </summary>
    public class DefaultAccountFactory
    {
        public Account Create(string playerServiceId)
        {
            //создать аккаунт с кораблями
            Account account = new Account
            {
                ServiceId = playerServiceId,
                Username = playerServiceId,
                RegistrationDateTime = DateTime.UtcNow,
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Hare,
                        CurrentSkinTypeId = SkinTypeEnum.Hare
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Bird,
                        CurrentSkinTypeId = SkinTypeEnum.Bird
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Smiley,
                        CurrentSkinTypeId = SkinTypeEnum.Smiley
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Sage,
                        CurrentSkinTypeId = SkinTypeEnum.Sage
                    }
                },
                Transactions = new List<Transaction>
                {
                    
                }
            };

            return account;
        }
    }
}