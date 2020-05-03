using System;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Route(nameof(Resource))]
        [HttpPost]
        public byte[] Resource([FromForm] string someField)
        {
            Console.WriteLine($"Вызов {nameof(someField)} = {someField}");
            byte[] test = {255,255,255,255,211};
            Response.ContentType = "application/octet-stream";
            return test;
        }
    }
}