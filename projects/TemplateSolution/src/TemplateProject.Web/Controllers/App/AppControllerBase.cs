using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.Web.Controllers.App
{
    [Route(WebConstants.SpaApiPathPrefix + "/[controller]")]
    public abstract class AppControllerBase : Controller
    {
    }
}