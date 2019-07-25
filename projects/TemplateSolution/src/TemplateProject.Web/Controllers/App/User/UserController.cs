using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Utils.Md5;
using TemplateProject.Web.Security;

namespace TemplateProject.Web.Controllers.App.User
{
    [Produces("application/json")]
    public sealed class UserController : ApiAppControllerBase
    {
        private readonly IDatabaseService _db;
        private readonly IMd5Crypter _md5Crypter;
        private readonly IWebSecurityService _webSecurityService;

        public UserController(IDatabaseService db,
            IMd5Crypter md5Crypter,
            IWebSecurityService webSecurityService)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _md5Crypter = md5Crypter ?? throw new ArgumentNullException(nameof(md5Crypter));
            _webSecurityService = webSecurityService ?? throw new ArgumentNullException(nameof(webSecurityService));
        }

        /// <summary>
        /// Changes password for logged user
        /// </summary>
        /// <param name="model">Password change data</param>
        /// <response code="200">Returns if password has been changed successfully</response>
        /// <response code="400">Returns if there is a validation error</response> 
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [HttpPost("password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordApiDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _webSecurityService.GetUserIdFromIdentity(User);
            if (userId == null)
            {
                ModelState.AddModelError(string.Empty, $"Cannot obtain user's id");
                return BadRequest(ModelState);
            }

            var user = _db.UsersRepository.GetById(userId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, $"User with id '{userId}' doesn't exist");
                return BadRequest(ModelState);
            }

            var oldPasswordEncrypted = _md5Crypter.Encrypt(model.OldPassword);
            if (oldPasswordEncrypted != user.PasswordEncrypted)
            {
                ModelState.AddModelError(nameof(ChangePasswordApiDto.OldPassword), "Old password is invalid");
                return BadRequest(ModelState);
            }

            user.PasswordEncrypted = _md5Crypter.Encrypt(model.NewPassword);

            _db.UsersRepository.Update(user);

            _db.SaveChanges();

            return Ok();
        }
    }
}