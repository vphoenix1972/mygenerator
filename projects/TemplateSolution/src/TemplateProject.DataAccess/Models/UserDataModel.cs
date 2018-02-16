using System.Collections.Generic;
using TemplateProject.Utils.Entities;

namespace TemplateProject.DataAccess.Models
{
    public sealed class UserDataModel : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string Password { get; set; }

        public IList<UserRoleUserDataModel> UserUserRoles { get; set; }

        public IList<RefreshTokenDataModel> RefreshTokens { get; set; }
    }
}