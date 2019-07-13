using System.Collections.Generic;
using TemplateProject.Utils.Entities;

namespace TemplateProject.Core.Domain
{
    public interface IUser : IEntity<string>
    {
        string Name { get; }

        string EMail { get; }

        string PasswordEncrypted { get; set; }

        IList<IUserRole> Roles { get; }
    }
}