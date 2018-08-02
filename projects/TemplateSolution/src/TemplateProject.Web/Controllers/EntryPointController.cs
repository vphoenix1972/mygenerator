using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.Web.Controllers
{
    [Route("")]
    public sealed class EntryPointController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/index.html");
        }
    }
}