using System.Collections.Generic;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public sealed class UsersFilter
    {
        public List<string> ExcludeUserRoleNames { get; set; }
    }
}