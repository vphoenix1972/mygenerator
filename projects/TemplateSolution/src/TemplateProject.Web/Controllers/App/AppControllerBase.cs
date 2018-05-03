using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.Web.Controllers.App
{
    [Route("app/[controller]")]
    public abstract class AppControllerBase : Controller
    {
    }
}