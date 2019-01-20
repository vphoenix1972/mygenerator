﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;

namespace TemplateProject.Web.Controllers.Admin.Users
{
    public sealed class UsersController : AdminControllerBase
    {
        private readonly IDatabaseService _db;
        private readonly IMapper _mapper;

        public UsersController(IDatabaseService db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            return Ok(_mapper.Map<List<UserModel>>(_db.UsersRepository.GetAll(filter)));
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