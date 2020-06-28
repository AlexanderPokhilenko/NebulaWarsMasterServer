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
    public class LobbyModelController : ControllerBase
    {
        private readonly LobbyModelFacadeService lobbyModelFacadeService;
        
        public LobbyModelController(LobbyModelFacadeService lobbyModelFacadeService)
        {
            this.lobbyModelFacadeService = lobbyModelFacadeService;
        }
        
        [Route(nameof(Create))]
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromForm] string playerServiceId)
        {
            Console.WriteLine($"{nameof(playerServiceId)} {playerServiceId}");
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }
            
            LobbyModel lobbyModel = await lobbyModelFacadeService.CreateAsync(playerServiceId);

            foreach (var warshipDto in lobbyModel.AccountDto.Warships)
            {
                if (warshipDto.PowerLevel == 0)
                {
                    throw new Exception("Нулевой уровень на внешнем слое");
                }
            }
            
            return lobbyModel.SerializeToBase64String();
        }
    }
}