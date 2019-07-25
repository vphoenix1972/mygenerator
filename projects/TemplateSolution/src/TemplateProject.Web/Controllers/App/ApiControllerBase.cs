using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.Web.Controllers.App
{
    [Route(WebConstants.ApiPrefix + "/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
    }
}