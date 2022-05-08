using Microsoft.AspNetCore.Mvc;

namespace OFM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtuhController : ControllerBase
    {
        public IActionResult Login()
        {
            return Created("", new TokenGenerator().GenereteToken());   
        }
    }
}
