using System;
using System.Linq;
using DataLayer.Tables;

namespace LibraryForTests
{
    public abstract class AccountDirector
    {
        protected readonly AccountBuilder Builder;

        protected AccountDirector(AccountBuilder builder)
        {
            Builder = builder;
        }
        
        public abstract void ConstructStart();
        public abstract void ConstructEnd();
        public virtual Account GetResult()
        {
            return Builder.GetResult();
        }
        
        public virtual int GetAccountRating()
        {
            return Builder.GetResult().Warships
                       .SelectMany(warship => warship.MatchResultForPlayers)
                       .Sum(matchResult => matchResult.WarshipRatingDelta);
        }

        public virtual int GetAccountRegularCurrency()
        {
            int fromMatches = Builder.GetResult().Warships
                .SelectMany(warship => warship.MatchResultForPlayers)
                .Sum(matchResult => matchResult.SoftCurrencyDelta);

            Console.WriteLine($"{nameof(fromMatches)} {fromMatches}");
            int fromLootboxes = Builder.GetResult().Lootboxes
                .SelectMany(lootbox => lootbox.LootboxPrizeSoftCurrency)
                .Sum(prize => prize.Quantity);
            
            Console.WriteLine($"{nameof(fromLootboxes)} {fromLootboxes}");
            //TODO посчитать покупки за реальную валюту
            return fromMatches + fromLootboxes;
        }
        
        public virtual int GetAccountPremiumCurrency()
        {
            //TODO посчитать лутбоксы
            //TODO посчитать покупки за реальную валюту
            return 0;
        }
    }
}