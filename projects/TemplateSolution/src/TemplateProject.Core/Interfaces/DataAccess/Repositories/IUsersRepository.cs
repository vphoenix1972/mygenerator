using System.Collections.Generic;
using TemplateProject.Core.Domain;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        IUser Get(string nameOrEMail, string password);

        IList<IUser> GetByNameOrEMail(string name, string eMail);

        IUser Add(IUser user);
    }
}