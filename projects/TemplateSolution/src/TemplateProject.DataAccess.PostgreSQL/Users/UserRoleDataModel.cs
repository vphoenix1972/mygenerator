using System.Collections.Generic;
using TemplateProject.Utils.Entities;

namespace TemplateProject.DataAccess.PostgreSQL.Users
{
    public sealed class UserRoleDataModel : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<UserRoleUserDataModel> UserRoleUsers { get; set; }
    }
}
