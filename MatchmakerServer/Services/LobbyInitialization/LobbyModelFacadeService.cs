using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для получения всей информации о аккаунте при загрузке игры.
    /// </summary>
    public class LobbyModelFacadeService
    {
        private readonly AccountFacadeService accountFacadeService;
        private readonly NotShownRewardDbUpdaterService notShownRewardDbUpdaterService;
        private readonly WarshipRatingScaleService warshipRatingScaleService;
        private readonly WarshipPowerScaleService warshipPowerScaleService;

        public LobbyModelFacadeService(AccountFacadeService accountFacadeService, 
            NotShownRewardDbUpdaterService notShownRewardDbUpdaterService, 
            WarshipRatingScaleService warshipRatingScaleService,
            WarshipPowerScaleService warshipPowerScaleService
        )
        {
            this.accountFacadeService = accountFacadeService;
            this.notShownRewardDbUpdaterService = notShownRewardDbUpdaterService;
            this.warshipRatingScaleService = warshipRatingScaleService;
            this.warshipPowerScaleService = warshipPowerScaleService;
        }

        public async Task<LobbyModel> Create([NotNull] string playerServiceId)
        {
            AccountModel accountModel = await accountFacadeService.GetOrCreateAccountData(playerServiceId);
            if (accountModel == null)
            {
                throw new Exception($"{nameof(accountModel)} is null");
            }
            
            RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShown = await notShownRewardDbUpdaterService
                .GetNotShownResultsAndMarkAsRead(playerServiceId);
            if (rewardsThatHaveNotBeenShown == null)
            { 
                throw new Exception("rewardsThatHaveNotBeenShown was null");
            }

            WarshipRatingScaleModel warshipRatingScaleModel = warshipRatingScaleService.GetWarshipRatingScaleModel();
            if (warshipRatingScaleModel == null)
            {
                throw new Exception($"{nameof(warshipRatingScaleModel)} was null");
            }

            WarshipPowerScaleModel warshipPowerScaleModel = warshipPowerScaleService.Create();
            if (warshipPowerScaleModel == null)
            {
                throw new Exception($"{nameof(warshipPowerScaleModel)} was null");
            }
            
            LobbyModel lobbyModel = new LobbyModel
            {
                AccountModel = accountModel,
                RewardsThatHaveNotBeenShown = rewardsThatHaveNotBeenShown,
                WarshipRatingScaleModel = warshipRatingScaleModel,
                WarshipPowerScaleModel = warshipPowerScaleModel
            };
            
            return lobbyModel;
        }
    }
}