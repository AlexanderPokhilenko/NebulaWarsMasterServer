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
        private readonly StubUsernameDbWriterService stubUsernameDbWriterService;
        
        public LobbyModelController(LobbyModelFacadeService lobbyModelFacadeService, 
            StubUsernameDbWriterService stubUsernameDbWriterService)
        {
            this.lobbyModelFacadeService = lobbyModelFacadeService;
            this.stubUsernameDbWriterService = stubUsernameDbWriterService;
        }
        
        [Route(nameof(Create))]
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromForm] string playerServiceId, [FromForm] string username)
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
                    throw new Exception("Нулевой уровень корабля");
                }
            }

            //обновить ник
            if (lobbyModel.AccountDto.Username != username && username != null && username.Length < 20)
            {
                lobbyModel.AccountDto.Username = username;
                await stubUsernameDbWriterService
                    .WriteAsync(lobbyModel.AccountDto.AccountId, username);
            }

            return lobbyModel.SerializeToBase64String();
        }
    }
}