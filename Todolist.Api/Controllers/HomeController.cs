using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Todolist.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : RESTFulController
    {
        [HttpGet]
        public ActionResult<string> Get() =>
            Ok("Hi Bro");
    }
}
