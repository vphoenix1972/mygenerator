using System;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Utils.Md5;
using TemplateProject.Web.Security;

namespace TemplateProject.Web.Controllers.App.User
{
    public sealed class UserController : AppControllerBase
    {
        private readonly IDatabaseService _db;
        private readonly IMd5Crypter _md5Crypter;
        private readonly IWebSecurityService _webSecurityService;

        public UserController(IDatabaseService db,
            IMd5Crypter md5Crypter,
            IWebSecurityService webSecurityService)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            if (md5Crypter == null)
                throw new ArgumentNullException(nameof(md5Crypter));
            if (webSecurityService == null)
                throw new ArgumentNullException(nameof(webSecurityService));

            _db = db;
            _md5Crypter = md5Crypter;
            _webSecurityService = webSecurityService;
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordApiModel model)
        {
            if (model == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _webSecurityService.GetUserIdFromIdentity(User);
            if (!userId.HasValue)
                return BadRequest();

            var user = _db.UsersRepository.GetById(userId.Value);
            if (user == null)
                return BadRequest($"User with id '{userId.Value}' doesn't exist");

            var oldPasswordEncrypted = _md5Crypter.Encrypt(model.OldPassword);
            if (oldPasswordEncrypted != user.PasswordEncrypted)
                return BadRequest("Old password is invalid");

            user.PasswordEncrypted = _md5Crypter.Encrypt(model.NewPassword);

            _db.UsersRepository.Update(user);

            _db.SaveChanges();

            return Ok();
        }
    }
}