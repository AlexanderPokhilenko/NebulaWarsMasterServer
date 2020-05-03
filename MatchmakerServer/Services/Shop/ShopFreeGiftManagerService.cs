using System;
using System.Threading.Tasks;
using DataLayer;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Управляет бесплатным подарком для игрока.
    /// </summary>
    public class ShopFreeGiftManagerService
    {
        private readonly ApplicationDbContext dbContext;

        public ShopFreeGiftManagerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<UiItemModel> GetOrCreate([NotNull] string playerServiceId)
        {
            throw new NotImplementedException();
        }
    }
}