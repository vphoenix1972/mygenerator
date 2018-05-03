using System.Collections.Generic;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public sealed class UsersFilter
    {
        public IList<string> ExcludeUserRoleNames { get; set; }
    }
}