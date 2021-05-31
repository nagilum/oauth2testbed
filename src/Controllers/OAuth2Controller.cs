using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace oauth2testbed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuth2Controller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> AuthorizeCodeFlow()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> AuthorizePasswordFlow()
        {
            throw new NotImplementedException();
        }
    }
}