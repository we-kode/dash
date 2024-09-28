using Dash.Application.Contracts;
using Dash.Server.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Dash.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository repository) : ControllerBase
    {
        [HttpPost("Login")]
        public ActionResult Login([FromBody] User user)
        {
            return Ok();
        }

        [HttpPost("Password/Update")]
        // [Authorize]
        public ActionResult UpdatePassword([FromBody] User user)
        {
            return Ok();
        }

        [HttpPost("Logout")]
        //[Authorize]
        public ActionResult Logout()
        {
            return Ok();
        }
    }
}
