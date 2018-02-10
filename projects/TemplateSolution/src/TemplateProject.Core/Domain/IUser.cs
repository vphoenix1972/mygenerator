using System.Collections.Generic;
using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Domain
{
    public interface IUser : IEntity<int?>
    {
        string Name { get; }

        string EMail { get; }

        string Password { get; }

        IList<IUserRole> Roles { get; }
    }
}