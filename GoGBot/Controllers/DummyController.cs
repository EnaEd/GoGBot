using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoGBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        [HttpGet("dummy")]
        public async Task<IActionResult> Get()
        {
            return Ok("work");
        }

    }
}
