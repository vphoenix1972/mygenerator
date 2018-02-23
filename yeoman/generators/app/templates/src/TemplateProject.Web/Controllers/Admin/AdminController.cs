using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleAdmin)]
    public abstract class AdminController : Controller
    {
    }
}