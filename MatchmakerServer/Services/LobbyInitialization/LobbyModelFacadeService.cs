using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для получения всей информации о аккаунте при входе в лобби.
    /// </summary>
    public class LobbyModelFacadeService
    {
        private readonly WarshipRatingScale warshipRatingScale;
        private readonly AccountFacadeService accountFacadeService;
        private readonly AccountMapperService accountMapperService;
        private readonly NotShownRewardsReaderService notShownRewardsReaderService;
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;

        public LobbyModelFacadeService(AccountFacadeService accountFacadeService,
            NotShownRewardsReaderService notShownRewardsReaderService, AccountMapperService accountMapperService)
        {
            warshipRatingScale = new WarshipRatingScale();
            this.accountFacadeService = accountFacadeService;
            this.accountMapperService = accountMapperService;
            this.notShownRewardsReaderService = notShownRewardsReaderService;
            warshipPowerScaleModelStorage = new WarshipPowerScaleModelStorage();
        }

        public async Task<LobbyModel> CreateAsync([NotNull] string playerServiceId)
        {
            AccountDbDto account = await accountFacadeService.ReadOrCreateAccountAsync(playerServiceId);
            if (account == null)
            {
                throw new NullReferenceException(nameof(account));
            }

            RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShown = await notShownRewardsReaderService
                .GetNotShownRewardAndMarkAsRead(playerServiceId);
          
            WarshipRatingScaleModel warshipRatingScaleModel = warshipRatingScale.GetWarshipRatingScaleModel();
            if (warshipRatingScaleModel == null)
            {
                throw new Exception($"{nameof(warshipRatingScaleModel)} was null");
            }

            WarshipPowerScaleModel warshipPowerScaleModel = warshipPowerScaleModelStorage.Create();
            if (warshipPowerScaleModel == null)
            {
                throw new Exception($"{nameof(warshipPowerScaleModel)} was null");
            }

            AccountDto accountDto = accountMapperService.Map(account);
            LobbyModel lobbyModel = new LobbyModel
            {
                AccountDto = accountDto,
                RewardsThatHaveNotBeenShown = rewardsThatHaveNotBeenShown,
                WarshipRatingScaleModel = warshipRatingScaleModel,
                WarshipPowerScaleModel = warshipPowerScaleModel
            };
            
            return lobbyModel;
        }
    }
}