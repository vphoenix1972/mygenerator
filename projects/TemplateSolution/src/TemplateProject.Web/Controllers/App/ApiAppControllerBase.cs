﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;

namespace TemplateProject.Web.Controllers.App
{
    [Route(WebConstants.ApiPrefix + "/app/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleUser)]
    public abstract class ApiAppControllerBase : Controller
    {
    }
}