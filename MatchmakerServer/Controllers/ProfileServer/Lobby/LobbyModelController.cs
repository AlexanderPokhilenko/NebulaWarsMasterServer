using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby
{
    /// <summary>
    /// Нужен для получения данных о кораблях, статистике аккаунта.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class LobbyModelController : ControllerBase
    {
        private readonly LobbyModelFacadeService lobbyModelFacadeService;
        private readonly UsernameChangingService usernameChangingService;

        public LobbyModelController(LobbyModelFacadeService lobbyModelFacadeService, 
            UsernameChangingService usernameChangingService)
        {
            this.lobbyModelFacadeService = lobbyModelFacadeService;
            this.usernameChangingService = usernameChangingService;
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
            if (lobbyModel.AccountDto.Username != username && username != null)
            {
                var validationResult = await usernameChangingService.ChangeUsername(playerServiceId, username);
                if (validationResult==UsernameValidationResultEnum.Ok)
                {
                    lobbyModel.AccountDto.Username = username;
                }
            }

            return lobbyModel.SerializeToBase64String();
        }
        
        [Route(nameof(SetUsername))]
        [HttpPost]
        public async Task<ActionResult<string>> SetUsername([FromForm] string playerServiceId, 
            [FromForm] string username)
        {
            Console.WriteLine($"{nameof(playerServiceId)} {playerServiceId}");
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }
            
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }
            
            UsernameValidationResultEnum validationResult = await usernameChangingService
                .ChangeUsername(playerServiceId, username);
            UsernameValidationResult usernameValidationResult = new UsernameValidationResult()
            {
                UsernameValidationResultEnum = validationResult
            };
            string result = usernameValidationResult.SerializeToBase64String();
            
            if (validationResult != UsernameValidationResultEnum.Ok)
            {
                Console.WriteLine("Ник не обновлён.");
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}