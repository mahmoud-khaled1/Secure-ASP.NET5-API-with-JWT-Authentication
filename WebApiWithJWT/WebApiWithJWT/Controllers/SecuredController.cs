using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecuredController : ControllerBase
    {
        [HttpGet("getdata")]
        public IActionResult GetData()
        {
            return Ok("Hello from secured controller");
        }
    }
}
