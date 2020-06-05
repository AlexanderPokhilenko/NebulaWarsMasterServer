using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly WarshipRatingScale warshipRatingScale;
        private readonly AccountFacadeService accountFacadeService;
        private readonly NotShownRewardService notShownRewardService;
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;

        public LobbyModelFacadeService(AccountFacadeService accountFacadeService,
            NotShownRewardService notShownRewardService,
            IMapper mapper)
        {
            this.accountFacadeService = accountFacadeService;
            this.notShownRewardService = notShownRewardService;
            warshipRatingScale = new WarshipRatingScale();
            warshipPowerScaleModelStorage = new WarshipPowerScaleModelStorage();
            this.mapper = mapper;
        }

        public async Task<LobbyModel> Create([NotNull] string playerServiceId)
        {
            Account account = await accountFacadeService.GetOrRegisterAccount(playerServiceId);
            if (account == null)
            {
                throw new Exception($"{nameof(account)} is null");
            }

            RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShown = await notShownRewardService
                .GetNotShownRewards(playerServiceId);
          
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
            
            AccountDto accountDto = mapper.Map<AccountDto>(account);
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