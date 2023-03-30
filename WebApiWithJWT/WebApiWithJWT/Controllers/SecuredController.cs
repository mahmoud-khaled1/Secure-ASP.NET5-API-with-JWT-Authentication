using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SecuredController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("getdata")]
        public IActionResult GetData()
        {
            return Ok("Hello from secured controller");
        }
    }
}
