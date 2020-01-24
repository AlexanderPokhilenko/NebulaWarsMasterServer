using System;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;
using ZeroFormatter;

//TODO поменять на get
//TODO убрать конвертацию в base64
//TODO убрать костыльные статусы 

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
                return StatusCode(1020);
            }
            else if (gameMatcher.PlayerInBattle(playerId))
            {
                Console.WriteLine("PlayerInBattle");
                GameRoomData roomData = gameMatcher.GetRoomData(playerId);
                byte[] data = ZeroFormatterSerializer.Serialize(roomData);
                Console.WriteLine("Размер массива = "+data.Length);
                string suka = Convert.ToBase64String(data);
                return suka;
            }
            else
            {
                Console.WriteLine("RegisterPlayer");
                gameMatcher.RegisterPlayer(playerId);
                return StatusCode(1000);
            }
        }
    }
}