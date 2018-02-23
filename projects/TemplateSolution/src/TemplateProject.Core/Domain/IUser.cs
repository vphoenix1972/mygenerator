using System.Collections.Generic;
using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Domain
{
    public interface IUser : IEntity<int?>
    {
        string Name { get; }

        string EMail { get; }

        string PasswordEncrypted { get; set; }

        IList<IUserRole> Roles { get; }
    }
}