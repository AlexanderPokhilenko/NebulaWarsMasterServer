using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
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
        private readonly StubLobbyModelDbWriteService stubLobbyModelDbWriteService;
        
        public LobbyModelController(LobbyModelFacadeService lobbyModelFacadeService, 
            StubLobbyModelDbWriteService stubLobbyModelDbWriteService)
        {
            this.lobbyModelFacadeService = lobbyModelFacadeService;
            this.stubLobbyModelDbWriteService = stubLobbyModelDbWriteService;
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
                await stubLobbyModelDbWriteService
                    .WriteAsync(lobbyModel.AccountDto.AccountId, username);
            }

            return lobbyModel.SerializeToBase64String();
        }
    }

    public class StubLobbyModelDbWriteService
    {
        private readonly ApplicationDbContext dbContext;

        public StubLobbyModelDbWriteService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task WriteAsync(int accountId, string username)
        {
            Account account = await dbContext.Accounts.FindAsync(accountId);
            account.Username = username;
            await dbContext.SaveChangesAsync();
        }
    }
}