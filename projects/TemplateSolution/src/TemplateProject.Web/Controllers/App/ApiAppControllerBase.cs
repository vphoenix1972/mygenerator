using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.Web.Controllers.App
{
    [Route(WebConstants.ApiPrefix + "/app/[controller]")]
    public abstract class ApiAppControllerBase : Controller
    {
    }
}