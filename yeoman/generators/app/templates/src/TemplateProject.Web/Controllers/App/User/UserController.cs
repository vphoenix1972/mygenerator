using Microsoft.AspNetCore.Mvc;
using System;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.Utils.Md5;
using <%= projectNamespace %>.Web.Security;

namespace <%= projectNamespace %>.Web.Controllers.App.User
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
        public IActionResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _webSecurityService.GetUserIdFromIdentity(User);
            if (!userId.HasValue)
            {
                ModelState.AddModelError(string.Empty, $"Cannot obtain user's id");
                return BadRequest(ModelState);
            }

            var user = _db.UsersRepository.GetById(userId.Value);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, $"User with id '{userId.Value}' doesn't exist");
                return BadRequest(ModelState);
            }

            var oldPasswordEncrypted = _md5Crypter.Encrypt(model.OldPassword);
            if (oldPasswordEncrypted != user.PasswordEncrypted)
            {
                ModelState.AddModelError(nameof(ChangePasswordModel.OldPassword), "Old password is invalid");
                return BadRequest(ModelState);
            }

            user.PasswordEncrypted = _md5Crypter.Encrypt(model.NewPassword);

            _db.UsersRepository.Update(user);

            _db.SaveChanges();

            return Ok();
        }
    }
}