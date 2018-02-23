using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace <%= projectNamespace %>.Web.Controllers
{
    [Route("")]
    public sealed class EntryPointController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View("App");
        }
    }
}