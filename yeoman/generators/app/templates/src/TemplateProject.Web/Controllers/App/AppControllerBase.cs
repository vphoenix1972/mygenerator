using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.App
{
    [Route(WebConstants.SpaApiPathPrefix + "/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleUser)]
    public abstract class AppControllerBase : Controller
    {
    }
}