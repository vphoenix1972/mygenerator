using System.Collections.Generic;
using <%= projectNamespace %>.Utils.Entities;

namespace <%= projectNamespace %>.Core.Domain
{
    public interface IUser : IEntity<string>
    {
        string Name { get; }

        string EMail { get; }

        string PasswordEncrypted { get; set; }

        IList<IUserRole> Roles { get; }
    }
}