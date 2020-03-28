using System;
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
        private readonly PlayerLobbyInitializeService playerLobbyInitializeService;

        public InitializeLobbyController(PlayerLobbyInitializeService playerLobbyInitializeService)
        {
            this.playerLobbyInitializeService = playerLobbyInitializeService;
        }

        [Route(nameof(GetAccountInfo))]
        [HttpPost]
        public async Task<ActionResult<string>> GetAccountInfo([FromForm] string playerId)
        {
            Console.WriteLine($"{nameof(playerId)} {playerId}");
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            AccountInfo accountInfo = await playerLobbyInitializeService.GetPlayerInfo(playerId);

            if (accountInfo == null)
                return BadRequest();

            return DichSerialize(accountInfo);
        }

        private string DichSerialize(AccountInfo accountInfo)
        {
            byte[] data = ZeroFormatter.ZeroFormatterSerializer.Serialize(accountInfo);
            string base64Dich = Convert.ToBase64String(data);
            return base64Dich;
        }
    }
}