using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;

namespace TemplateProject.Web.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleAdmin)]
    public abstract class AdminController : Controller
    {
    }
}