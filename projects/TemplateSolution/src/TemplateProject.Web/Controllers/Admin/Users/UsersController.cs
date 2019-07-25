using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;

namespace TemplateProject.Web.Controllers.Admin.Users
{
    [Produces("application/json")]
    public sealed class UsersController : ApiAdminControllerBase
    {
        private readonly IDatabaseService _db;
        private readonly IMapper _mapper;

        public UsersController(IDatabaseService db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets all users except admin.
        /// </summary>
        /// <remarks>
        /// Requires admin role
        /// </remarks>
        /// <response code="200">Returns a list of users</response>
        [ProducesResponseType(typeof(IEnumerable<UserApiDto>), 200)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var filter = new UsersFilter()
            {
                ExcludeUserRoleNames = new List<string>()
                {
                    UserRoleNames.RoleAdmin
                }
            };

            return Ok(_mapper.Map<List<UserApiDto>>(_db.UsersRepository.GetAll(filter)));
        }

        /// <summary>
        /// Deletes a user by id.
        /// </summary>
        /// <remarks>
        /// Requires admin role
        /// </remarks>
        /// <response code="200">Returns if user has been deleted successfully of does not exist</response>
        [ProducesResponseType(typeof(void), 200)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _db.RefreshTokensRepository.DeleteByUserId(id);
            _db.UsersRepository.DeleteById(id);

            _db.SaveChanges();

            return Ok();
        }
    }
}