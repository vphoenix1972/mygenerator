﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;

namespace TemplateProject.Web.Controllers.App
{
    [Route(WebConstants.SpaApiPathPrefix + "/[controller]")]
    [Authorize(Roles = UserRoleNames.RoleUser)]
    public abstract class AppControllerBase : Controller
    {
    }
}