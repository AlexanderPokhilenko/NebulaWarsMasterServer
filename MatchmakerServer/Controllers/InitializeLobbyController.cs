using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
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
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            AccountInfo accountInfo = await playerLobbyInitializeService.GetPlayerInfo(playerId);

            if (accountInfo == null)
                return BadRequest();

            return GetStub(accountInfo);
        }

        private string GetStub(AccountInfo accountInfo)
        {
            byte[] data = ZeroFormatter.ZeroFormatterSerializer.Serialize(accountInfo);
            string base64Dich = Convert.ToBase64String(data);
            return base64Dich;
        }
    }
}