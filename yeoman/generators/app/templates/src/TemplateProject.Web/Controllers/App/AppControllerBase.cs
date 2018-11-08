using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.App
{
    [Route(WebConstants.SpaApiPathPrefix + "/[controller]")]
    public abstract class AppControllerBase : Controller
    {
    }
}