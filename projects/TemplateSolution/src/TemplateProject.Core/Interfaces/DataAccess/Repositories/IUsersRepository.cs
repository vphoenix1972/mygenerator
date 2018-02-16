using System.Collections.Generic;
using TemplateProject.Core.Domain;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        IUser GetById(int id);

        IUser Get(string nameOrEMail, string password);

        IList<IUser> GetByNameOrEMail(string name, string eMail);

        IUser Add(IUser user);
    }
}