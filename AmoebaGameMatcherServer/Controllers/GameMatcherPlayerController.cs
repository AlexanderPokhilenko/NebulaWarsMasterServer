using System;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;
using ZeroFormatter;

//TODO поменять на get

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

        [Route("GetRoomData")]
        [HttpPost]
        public ActionResult<string> GetRoomData([FromForm]string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            if (gameMatcher.PlayerInQueue(playerId))
            {
                Console.WriteLine("PlayerInQueue");
                GameMatherResponse response = new GameMatherResponse();
                response.PlayerInQueue = true;
                byte[] data = ZeroFormatterSerializer.Serialize(response);
                string stub = Convert.ToBase64String(data);
                return stub;
            }
            else if (gameMatcher.PlayerInBattle(playerId))
            {
                Console.WriteLine("PlayerInBattle");
                GameRoomData roomData = gameMatcher.GetRoomData(playerId);
                GameMatherResponse response = new GameMatherResponse();
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
                GameMatherResponse response = new GameMatherResponse();
                response.PlayerHasJustBeenRegistered = true;
                byte[] data = ZeroFormatterSerializer.Serialize(response);
                string stub = Convert.ToBase64String(data);
                return stub;
            }
        }
    }
}