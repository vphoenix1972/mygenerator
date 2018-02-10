using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.Web.Controllers.Security
{
    [Route("security")]
    public sealed class SecurityController : Controller
    {
        private readonly IDatabaseService _db;

        public SecurityController(IDatabaseService db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _db = db;
        }

        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInApiModel model)
        {
            if (model == null)
                return BadRequest();

            var user = _db.UsersRepository.Get(model.Login, model.Password);
            if (user == null)
                return BadRequest();

            return Ok(new
            {
                user.Id,
                user.Name,
                user.EMail,
                Roles = user.Roles.Select(e => e.Name)
            });
        }
    }
}