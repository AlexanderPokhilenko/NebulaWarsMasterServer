using System;
using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
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

        public LobbyModelFacadeService(AccountFacadeService accountFacadeService,
            NotShownRewardsReaderService notShownRewardsReaderService, AccountMapperService accountMapperService)
        {
            warshipRatingScale = new WarshipRatingScale();
            this.accountFacadeService = accountFacadeService;
            this.accountMapperService = accountMapperService;
            this.notShownRewardsReaderService = notShownRewardsReaderService;
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

            AccountDto accountDto = accountMapperService.Map(account);
            LobbyModel lobbyModel = new LobbyModel
            {
                AccountDto = accountDto,
                RewardsThatHaveNotBeenShown = rewardsThatHaveNotBeenShown,
                WarshipRatingScaleModel = warshipRatingScaleModel
            };
            
            return lobbyModel;
        }
    }
}