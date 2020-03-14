// using Microsoft.AspNetCore.Mvc;
//
// namespace AmoebaGameMatcherServer.Controllers
// {
//     [Route("[controller]")]
//     [ApiController]
//     public class LobbyWarshipsController : ControllerBase
//     {
//         [Route(nameof(GetWarshipsInfo))]
//         [HttpPost]
//         public ActionResult GetWarshipsInfo([FromForm]string playerId)
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