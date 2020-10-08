using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    public class SkinsDbReaderService : ISkinsDbReaderService
    {
        private readonly ApplicationDbContext dbContext;

        public SkinsDbReaderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Dictionary<int, List<SkinType>>> ReadAsync(int accountId)
        {
            List<Increment> increments = await dbContext.Increments
                .Include(increment => increment.SkinType)
                .Include(increment => increment.Transaction)
                .Include(increment => increment.SkinType)
                .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.Skin 
                                    && increment.Transaction.AccountId == accountId)
                .ToListAsync();
         
            
            //warshipId, skinNames
            Dictionary<int, List<SkinType>> dict = new Dictionary<int, List<SkinType>>();

            foreach (Increment increment in increments
                .OrderBy(increment=>increment.Transaction.DateTime))
            {
                if (increment.WarshipId == null)
                {
                    throw new NullReferenceException(nameof(increment.WarshipId));
                }
              
                if (dict.TryGetValue(increment.WarshipId.Value, out var list))
                {
                    list.Add(increment.SkinType);
                }
                else
                {
                    dict.Add(increment.WarshipId.Value, new List<SkinType> {increment.SkinType});
                }
            }

            return dict;
        }
    }
}