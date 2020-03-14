// using System.ComponentModel;
// using Microsoft.AspNetCore.Mvc;
//
// namespace AmoebaGameMatcherServer.Controllers
// {
//     [Route("[controller]")]
//     [ApiController]
//     public class LootboxController : ControllerBase
//     {
//         [Route(nameof(OpenSmallLootbox))]
//         [HttpPost]
//         public ActionResult OpenSmallLootbox([FromForm]string playerId)
//         {
//             if (string.IsNullOrEmpty(playerId))
//                 return BadRequest();
//
//             //TODO передать запрос в сервис
//             
//             return new StatusCodeResult(500);
//         }
//     }
// }