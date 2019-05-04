﻿using System.Collections.Generic;
using TemplateProject.DataAccess.PostgreSQL.RefreshTokens;
using TemplateProject.Utils.Entities;

namespace TemplateProject.DataAccess.PostgreSQL.Users
{
    public sealed class UserDataModel : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string PasswordEncrypted { get; set; }

        public IList<UserRoleUserDataModel> UserUserRoles { get; set; }

        public IList<RefreshTokenDataModel> RefreshTokens { get; set; }
    }
}
