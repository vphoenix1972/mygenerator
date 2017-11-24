using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.Web.Controllers
{
    [Route("")]
    public sealed class AppController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("App");
        }
    }
}