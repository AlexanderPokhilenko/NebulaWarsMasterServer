using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RewardsThatHaveNotBeenShownController:ControllerBase
    {
        private readonly NotShownRewardDbUpdaterService dbReaderUtilService;

        public RewardsThatHaveNotBeenShownController(NotShownRewardDbUpdaterService dbReaderUtilService)
        {
            this.dbReaderUtilService = dbReaderUtilService;
        }
        
        [Route(nameof(Get))]
        [HttpPost]
        public async Task<ActionResult<string>> Get([FromQuery] string playerServiceId)
        {
            if (playerServiceId == null)
            {
                return BadRequest();
            }

            RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShown = await dbReaderUtilService
                .GetNotShownResults(playerServiceId);

            if (rewardsThatHaveNotBeenShown == null)
            {
                return BadRequest();
            }
            
            return DichSerialize(rewardsThatHaveNotBeenShown);
        }
        
        private string DichSerialize<T>(T response)
        {
            byte[] data = ZeroFormatterSerializer.Serialize(response);
            string stub = Convert.ToBase64String(data);
            return stub;  
        }
    }
}