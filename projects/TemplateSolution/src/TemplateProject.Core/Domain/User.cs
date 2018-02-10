using System.Collections.Generic;

namespace TemplateProject.Core.Domain
{
    public sealed class User : IUser
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string Password { get; set; }

        public IList<IUserRole> Roles { get; set; } = new List<IUserRole>();
    }
}