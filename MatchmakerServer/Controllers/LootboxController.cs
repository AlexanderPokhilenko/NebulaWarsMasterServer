using System;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Route(nameof(Test))]
        [HttpGet]
        public ActionResult Test([FromForm]string playerId)
        {
            Console.WriteLine("Был вызван");
            foreach (var pair in Request.Query)
            {
                Console.WriteLine(pair.Key+" "+pair.Value);
            }

            return Ok();
        }
    }
}