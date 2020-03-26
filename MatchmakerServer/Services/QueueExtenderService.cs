using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public interface IWarshipValidatorService
    {
        Task<(bool success, Warship warship)> GetWarshipById(string playerServiceId, int warshipId);
    }
    
    public class WarshipValidatorServiceStub:IWarshipValidatorService
    {
#pragma warning disable 1998
        public async Task<(bool success, Warship warship)> GetWarshipById(string playerServiceId, int warshipId)
#pragma warning restore 1998
        {
            Warship warship = new Warship()
            {
                WarshipType = new WarshipType()
            };
            return new ValueTuple<bool, Warship>(true, warship);
        }
    }
    
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
                return new ValueTuple<bool, Warship>(true, warship);
            }
        }
    }
    /// <summary>
    /// Отвечает за проверку данных игрока перед добавлением в очередь (пока один режим)
    /// </summary>
    public class QueueExtenderService
    {
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService;
        private readonly IWarshipValidatorService warshipValidatorService;

        public QueueExtenderService(BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService,
            IWarshipValidatorService warshipValidatorService)
        {
            this.battleRoyaleQueueSingletonServiceService = battleRoyaleQueueSingletonServiceService;
            this.warshipValidatorService = warshipValidatorService;
        }
        
        /// <summary>
        /// Проверяет данные и добавляет игрока в очередь.
        /// </summary>
        /// <param name="playerServiceId"></param>
        /// <param name="warshipId"></param>
        /// <returns>Вернёт false если в БД нет такиз данных или игрок уже в очереди.</returns>
        public async Task<bool> TryEnqueuePlayer(string playerServiceId, int warshipId)
        {
            //В БД есть эти данные?
            var (success, warship) = await warshipValidatorService.GetWarshipById(playerServiceId, warshipId);
            if (!success)
            {
                return false;
            }
            
            return battleRoyaleQueueSingletonServiceService.TryEnqueuePlayer(playerServiceId, warship);
        }
    }
}