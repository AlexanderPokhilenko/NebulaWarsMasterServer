using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Нужен при добавлении игрока в очередь.
    /// </summary>
    public class WarshipValidatorService:IWarshipValidatorService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public WarshipValidatorService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Достаёт из БД корабль и аккаунт.
        /// </summary>
        /// <param name="playerServiceId"></param>
        /// <param name="warshipId"></param>
        /// <returns></returns>
        public async Task<(bool success, Warship warship)> GetWarshipById(string playerServiceId, int warshipId)
        {
            var warship = await applicationDbContext.Warships
                .Include(warship1 => warship1.WarshipType)
                .Include(warship1 =>warship1.Account)
                .SingleOrDefaultAsync(warship1 => 
                    warship1.Id == warshipId 
                    && warship1.Account.ServiceId==playerServiceId);
            
            if (warship == null)
            {
                return new ValueTuple<bool, Warship>(false, null);
            }
            else
            {
                if (warship.Account == null)
                {
                    throw new NullReferenceException(nameof(warship.Account));
                }
                if (warship.Account.ServiceId == null)
                {
                    throw new NullReferenceException(nameof(warship.Account.ServiceId));
                }
                return new ValueTuple<bool, Warship>(true, warship);
            }
        }
    }
}