using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.App
{
    [Route("app/[controller]")]
    public abstract class AppControllerBase : Controller
    {
    }
}