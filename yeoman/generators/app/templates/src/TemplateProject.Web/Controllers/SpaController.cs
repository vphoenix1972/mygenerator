using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace <%= projectNamespace %>.Web.Controllers
{
    public sealed class SpaController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}