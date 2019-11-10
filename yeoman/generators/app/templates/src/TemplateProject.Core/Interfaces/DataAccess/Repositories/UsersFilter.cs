using System.Collections.Generic;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public sealed class UsersFilter
    {
        public List<string> ExcludeUserRoleNames { get; set; }
    }
}