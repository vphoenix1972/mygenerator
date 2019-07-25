using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.Admin
{
    [Route(WebConstants.ApiPrefix + "/admin/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleAdmin)]
    public abstract class ApiAdminControllerBase : Controller
    {
    }
}