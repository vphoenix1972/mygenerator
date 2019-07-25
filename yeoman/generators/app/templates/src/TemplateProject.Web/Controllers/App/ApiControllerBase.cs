using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.App
{
    [Route(WebConstants.ApiPrefix + "/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleUser)]
    public abstract class ApiControllerBase : Controller
    {
    }
}