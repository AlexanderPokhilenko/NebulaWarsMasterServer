using System;
using System.Threading.Tasks;
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
        private readonly LobbyModelFacadeService lobbyModelFacadeService;
        
        public LobbyDataController(LobbyModelFacadeService lobbyModelFacadeService)
        {
            this.lobbyModelFacadeService = lobbyModelFacadeService;
        }

        //TODO говно
        [Route(nameof(Create))]
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromForm] string playerServiceId)
        {
            Console.WriteLine($"{nameof(playerServiceId)} {playerServiceId}");
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }
            
            var lobbyModel = await lobbyModelFacadeService.Create(playerServiceId);
            return DichSerialize(lobbyModel);
        }

        private string DichSerialize(LobbyModel lobbyModel)
        {
            byte[] data = ZeroFormatter.ZeroFormatterSerializer.Serialize(lobbyModel);
            string base64Dich = Convert.ToBase64String(data);
            return base64Dich;
        }
    }
}