﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;

namespace TemplateProject.Web.Controllers.Admin
{
    [Route(WebConstants.ApiPrefix + "/admin/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleAdmin)]
    public abstract class ApiAdminControllerBase : Controller
    {
    }
}