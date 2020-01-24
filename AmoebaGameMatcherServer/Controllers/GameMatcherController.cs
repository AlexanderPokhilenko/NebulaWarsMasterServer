using System;
using System.Text;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameMatcherController : ControllerBase
    {
        private readonly GameMatcherService gameMatcher;

        public GameMatcherController(GameMatcherService gameMatcher)
        {
            this.gameMatcher = gameMatcher;
        }

        /// <summary>
        /// Метод вызывается из udp сервером при окончании игровой сессии
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRoom([FromForm]string secretKey, [FromForm] int roomNumber)
        {
            bool requestCameFromARealGameServer = CheckSecretKey(secretKey);
            if (!requestCameFromARealGameServer)
                return new ForbidResult();

            gameMatcher.DeleteRoom(roomNumber);
            return Ok();
        }
        
        [Route("GetRoomData")]
        //TODO поменять на get
        [HttpPost]
        public ActionResult<string> GetRoomData([FromForm]string playerId)
        {
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

        private bool CheckSecretKey(string secretKey)
        {
            return Globals.secretKey == secretKey;
        }
    }
}