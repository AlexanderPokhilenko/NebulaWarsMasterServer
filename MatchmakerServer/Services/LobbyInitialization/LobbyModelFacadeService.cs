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
        private readonly NotShownRewardsReaderService notShownRewardsReaderService;
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;
        private readonly AccountMapper accountMapper;

        public LobbyModelFacadeService(AccountFacadeService accountFacadeService,
            NotShownRewardsReaderService notShownRewardsReaderService)
        {
            this.accountFacadeService = accountFacadeService;
            this.notShownRewardsReaderService = notShownRewardsReaderService;
            warshipRatingScale = new WarshipRatingScale();
            warshipPowerScaleModelStorage = new WarshipPowerScaleModelStorage();
            accountMapper = new AccountMapper();
        }

        public async Task<LobbyModel> CreateAsync([NotNull] string playerServiceId)
        {
            var account = await accountFacadeService.ReadOrCreateAccountAsync(playerServiceId);
            if (account == null)
            {
                throw new Exception($"{nameof(account)} is null");
            }

            RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShown = await notShownRewardsReaderService
                .GetNotShownResults(playerServiceId);
          
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

            AccountDto accountDto = accountMapper.Map(account);
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