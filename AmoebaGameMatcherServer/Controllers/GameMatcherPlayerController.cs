using System;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

//TODO поменять на get
//TODO убрать base64

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameMatcherPlayerController : ControllerBase
    {
        private readonly GameMatcherService gameMatcher;

        public GameMatcherPlayerController(GameMatcherService gameMatcher)
        {
            this.gameMatcher = gameMatcher;
        }

        [Route("ExitFromBattle")]
        [HttpPost]
        public ActionResult ExitFromBattle([FromForm]string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            if (gameMatcher.TryRemovePlayerFromBattle(playerId))
            {
                Console.WriteLine("Игрок успешно удалён из комнаты.\n\n\n\n\n\n\n\n");
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        
        [Route("DeleteFromQueue")]
        [HttpPost]
        public ActionResult<string> DeleteFromQueue([FromForm]string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            if (gameMatcher.TryRemovePlayerFromQueue(playerId))
            {
                return Ok();
            }
            else
            {
                return StatusCode(409);
            }
        }
        
        [Route("GetRoomData")]
        [HttpPost]
        public ActionResult<string> GetRoomData([FromForm]string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            GameMatcherResponse response = new GameMatcherResponse
            {
                NumberOfPlayersInQueue = gameMatcher.GetNumberOfPlayersInQueue(),
                NumberOfPlayersInBattles = gameMatcher.GetNumberOfPlayersInBattles()
            };
            
            if (gameMatcher.PlayerInQueue(playerId))
            {
                Console.WriteLine("PlayerInQueue");
                response.PlayerInQueue = true;
                byte[] data = ZeroFormatterSerializer.Serialize(response);
                string stub = Convert.ToBase64String(data);
                return stub;
            }
            else if (gameMatcher.PlayerInBattle(playerId))
            {
                Console.WriteLine("PlayerInBattle");
                GameRoomData roomData = gameMatcher.GetRoomData(playerId);
                response.PlayerInBattle = true;
                response.GameRoomData = roomData;
                byte[] data = ZeroFormatterSerializer.Serialize(response);
                string stub = Convert.ToBase64String(data);
                return stub;
            }
            else
            {
                Console.WriteLine("RegisterPlayer");
                gameMatcher.RegisterPlayer(playerId);
                response.PlayerHasJustBeenRegistered = true;
                byte[] data = ZeroFormatterSerializer.Serialize(response);
                string stub = Convert.ToBase64String(data);
                return stub;
            }
        }
    }
}