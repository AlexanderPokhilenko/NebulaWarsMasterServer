using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для получения данных о кораблях, статистике аккаунта.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class InitializeLobbyController : ControllerBase
    {
        private readonly PlayerInfoManagerService playerInfoManagerService;

        public InitializeLobbyController(PlayerInfoManagerService playerInfoManagerService)
        {
            this.playerInfoManagerService = playerInfoManagerService;
        }

        [Route(nameof(GetAccountInfo))]
        [HttpPost]
        public async Task<ActionResult<string>> GetAccountInfo([FromForm] string playerId)
        {
            Console.WriteLine($"{nameof(playerId)} {playerId}");
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest();
            }
            AccountInfo accountInfo = await playerInfoManagerService.GetOrCreateAccountInfo(playerId);
            if (accountInfo == null)
            {
                throw new NullReferenceException(nameof(accountInfo));
            }
            
            Console.WriteLine(accountInfo.Username);
            string answer = DichSerialize(accountInfo);
            Console.WriteLine($"{nameof(answer.Length)} {answer.Length}");
            return answer;
        }

        private string DichSerialize(AccountInfo accountInfo)
        {
            byte[] data = ZeroFormatter.ZeroFormatterSerializer.Serialize(accountInfo);
            string base64Dich = Convert.ToBase64String(data);
            return base64Dich;
        }
    }
}