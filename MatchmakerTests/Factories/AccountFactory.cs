using System.Collections.Generic;
using DataLayer.Tables;

namespace MatchmakerTest.Utils
{
    public static class AccountFactory
    {
        public static Account CreateSimpleAccount()
        {
            return new Account()
            {
                Username = "Игорь",
                Warships = new List<Warship>()
                {
                    new Warship()
                    {
                        WarshipTypeId = 1
                    }
                }
            };
        }
    }
}