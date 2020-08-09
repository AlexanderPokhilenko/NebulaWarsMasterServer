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
        public ActionResult Test()
        {
            foreach (var pair in HttpContext.Request.Query)
            {
                Console.WriteLine(pair.Key+" "+pair.Value);
                foreach (var value in pair.Value)
                {
                    Console.WriteLine(value);
                }
                Console.WriteLine("--");
            }
            
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            Console.WriteLine("вызов");
            return Ok();
        }
    }
}