using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithWatchDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            return Ok(new { username, password });
        }
    }
}
