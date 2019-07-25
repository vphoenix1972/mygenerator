using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;

namespace TemplateProject.Web.Controllers.App
{
    [Route(WebConstants.ApiPrefix + "/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleUser)]
    public abstract class ApiControllerBase : Controller
    {
    }
}