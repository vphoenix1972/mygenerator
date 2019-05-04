using System.Collections.Generic;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.DataAccess.SQLServer.Users
{
    public sealed class UserRoleDataModel : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<UserRoleUserDataModel> UserRoleUsers { get; set; }
    }
}
