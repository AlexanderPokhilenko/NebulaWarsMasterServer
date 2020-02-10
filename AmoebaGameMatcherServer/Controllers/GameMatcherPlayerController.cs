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

        [Route("GetRoomData")]
        [HttpPost]
        public ActionResult<string> GetRoomData([FromForm]string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            if (gameMatcher.PlayerInQueue(playerId))
            {
                Console.WriteLine("PlayerInQueue");
                GameMatcherResponse response = new GameMatcherResponse {PlayerInQueue = true};
                byte[] data = ZeroFormatterSerializer.Serialize(response);
                string stub = Convert.ToBase64String(data);
                return stub;
            }
            else if (gameMatcher.PlayerInBattle(playerId))
            {
                Console.WriteLine("PlayerInBattle");
                GameRoomData roomData = gameMatcher.GetRoomData(playerId);
                GameMatcherResponse response = new GameMatcherResponse {PlayerInBattle = true, GameRoomData = roomData};
                byte[] data = ZeroFormatterSerializer.Serialize(response);
                string stub = Convert.ToBase64String(data);
                return stub;
            }
            else
            {
                Console.WriteLine("RegisterPlayer");
                gameMatcher.RegisterPlayer(playerId);
                GameMatcherResponse response = new GameMatcherResponse {PlayerHasJustBeenRegistered = true};
                byte[] data = ZeroFormatterSerializer.Serialize(response);
                string stub = Convert.ToBase64String(data);
                return stub;
            }
        }
    }

    
}