using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer
{
    public class MatchRewardTypeSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.MatchRewardTypes.Any())
            {
                List<MatchRewardType> matchRewardTypes = new List<MatchRewardType>
                {
                    new MatchRewardType
                    {
                        Id = MatchRewardTypeEnum.RankingReward,
                        Name = MatchRewardTypeEnum.RankingReward.ToString()
                    },
                    new MatchRewardType
                    {
                        Id = MatchRewardTypeEnum.DoubleLootboxPoints,
                        Name = MatchRewardTypeEnum.DoubleLootboxPoints.ToString()
                    }
                };
                dbContext.MatchRewardTypes.AddRange(matchRewardTypes);
                dbContext.SaveChanges();
            }
        }
    }
}