using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Api.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get()
        {
            return Ok("Welcome to the Chameleon Api.");
        }
    }
}
