using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для сохранения текущего скина.
    /// </summary>
    public class CurrentSkinChangingService
    {
        private readonly ApplicationDbContext dbContext;

        public CurrentSkinChangingService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> TryChange(string playerServiceId, int warshipId, string skinName)
        {
            //Корабль принаджлежит аккаунту?
            Warship warship = await dbContext.Warships
                .Include(warship1 => warship1.Account)
                .Where(warship1 => warship1.Id == warshipId && warship1.Account.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();

            if (warship == null)
            {
                return false;
            }
            
            //У аккаунта куплен этот скин?
            SkinType skinType = await dbContext.Increments
                .Include(increment => increment.Transaction)
                .Include(increment => increment.SkinType)
                .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.Skin
                                    && increment.Transaction.AccountId == warship.AccountId
                                    && increment.WarshipId==warshipId
                                    && increment.SkinType.Name==skinName)
                .Select(increment => increment.SkinType)
                .SingleOrDefaultAsync();

            if (skinType == null)
            {
                return false;
            }

            warship.CurrentSkinTypeId = skinType.Id;
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}