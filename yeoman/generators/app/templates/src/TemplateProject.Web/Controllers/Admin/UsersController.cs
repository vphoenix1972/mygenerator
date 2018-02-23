using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories;

namespace <%= projectNamespace %>.Web.Controllers.Admin
{
    public sealed class UsersController : AdminController
    {
        private readonly IDatabaseService _db;

        public UsersController(IDatabaseService db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _db = db;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            var filter = new UsersFilter()
            {
                ExcludeUserRoleNames = new List<string>()
                {
                    UserRoleNames.RoleAdmin
                }
            };

            return Ok(_db.UsersRepository.GetAll(filter));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _db.RefreshTokensRepository.DeleteByUserId(id);
            _db.UsersRepository.DeleteById(id);

            _db.SaveChanges();

            return Ok();
        }
    }
}