using System;
using System.Threading.Tasks;
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

        [Route(nameof(ExitFromBattle))]
        [HttpPost]
        public ActionResult ExitFromBattle([FromForm]string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            if (gameMatcher.TryRemovePlayerFromBattle(playerId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        
        [Route(nameof(DeleteFromQueue))]
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

       

        private string DichSerialize(GameMatcherResponse response)
        {
            byte[] data = ZeroFormatterSerializer.Serialize(response);
            string stub = Convert.ToBase64String(data);
            return stub;  
        }
        
        
        [Route(nameof(GetRoomData))]
        [HttpPost]
        public async Task<ActionResult<string>> GetRoomData([FromForm]string playerId, [FromForm] int warshipId)
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
                return DichSerialize(response);
            }
            else if (gameMatcher.PlayerInBattle(playerId))
            {
                Console.WriteLine("PlayerInBattle");
                GameRoomData roomData = gameMatcher.GetRoomData(playerId);
                response.PlayerInBattle = true;
                response.GameRoomData = roomData;
                return DichSerialize(response);
            }
            else
            {
                Console.WriteLine("RegisterPlayer");
                if (!await gameMatcher.TryRegisterPlayer(playerId, warshipId))
                {
                    throw new Exception("Не удалось зарегистрировать игрока.");
                }
                response.PlayerHasJustBeenRegistered = true;
                return DichSerialize(response);
            }
        }
    }
}