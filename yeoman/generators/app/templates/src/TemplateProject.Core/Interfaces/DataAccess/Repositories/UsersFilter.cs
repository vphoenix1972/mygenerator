using System.Collections.Generic;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public sealed class UsersFilter
    {
        public IList<string> ExcludeUserRoleNames { get; set; }
    }
}