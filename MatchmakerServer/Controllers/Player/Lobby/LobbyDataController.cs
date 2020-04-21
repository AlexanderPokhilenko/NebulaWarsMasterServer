using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для получения данных о кораблях, статистике аккаунта.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class LobbyDataController : ControllerBase
    {
        private readonly AccountFacadeService accountFacadeService;
        private readonly NotShownRewardDbUpdaterService notShownRewardDbUpdaterService;

        public LobbyDataController(AccountFacadeService accountFacadeService,
            NotShownRewardDbUpdaterService notShownRewardDbUpdaterService)
        {
            this.accountFacadeService = accountFacadeService;
            this.notShownRewardDbUpdaterService = notShownRewardDbUpdaterService;
        }

        [Route(nameof(Get))]
        [HttpPost]
        public async Task<ActionResult<string>> Get([FromForm] string playerServiceId)
        {
            Console.WriteLine($"{nameof(playerServiceId)} {playerServiceId}");
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }
            
            RelevantAccountData accountData = await accountFacadeService.GetOrCreateAccountData(playerServiceId);
            
            if (accountData == null)
            {
                Console.WriteLine($"{nameof(accountData)} is null");
                return StatusCode(500);
            }
            
            RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShown = await notShownRewardDbUpdaterService
                .GetNotShownResults(playerServiceId);

            if (rewardsThatHaveNotBeenShown == null)
            {
                Console.WriteLine("rewardsThatHaveNotBeenShown was null");
                return StatusCode(500);
            }

            LobbyData lobbyData = new LobbyData()
            {
                RelevantAccountData = accountData,
                RewardsThatHaveNotBeenShown = rewardsThatHaveNotBeenShown
            };
            return DichSerialize(lobbyData);
            
        }

        private string DichSerialize(LobbyData lobbyData)
        {
            byte[] data = ZeroFormatter.ZeroFormatterSerializer.Serialize(lobbyData);
            string base64Dich = Convert.ToBase64String(data);
            return base64Dich;
        }
    }
}