using System.Collections.Generic;
using DataLayer.Tables;

namespace MatchmakerTest.Utils
{
    /// <summary>
    /// Нужно, чтобы создавать аккаунты для тестов
    /// </summary>
    public static class AccountFactory
    {
        public static Account CreateSimpleAccount()
        {
            return new Account
            {
                ServiceId = UniqueStringFactory.Create(),
                Username = "Игорь",
                Warships = new List<Warship>
                {
                    new Warship()
                    {
                        WarshipTypeId = 1,
                        Rating = 65
                    }
                }
            };
        }

        public static List<Account> CreateAccounts(int count)
        {
            List<Account> result = new List<Account>();
            for (int i = 0; i < count; i++)
            {
                var account = new Account()
                {
                    ServiceId = UniqueStringFactory.Create(),
                    Username = "Игорь",
                    Warships = new List<Warship>
                    {
                        new Warship()
                        {
                            WarshipTypeId = 1,
                            Rating = 45
                        }
                    }
                };
                result.Add(account);
            }

            return result;
        }
    }
}