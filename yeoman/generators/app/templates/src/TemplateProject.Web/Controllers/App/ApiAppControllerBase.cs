using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.App
{
    [Route(WebConstants.ApiPrefix + "/app/[controller]")]
    public abstract class ApiAppControllerBase : Controller
    {
    }
}