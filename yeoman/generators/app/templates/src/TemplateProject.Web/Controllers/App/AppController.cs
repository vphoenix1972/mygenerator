using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.App
{
    [Route("app/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleUser)]
    public abstract class AppController : Controller
    {
    }
}