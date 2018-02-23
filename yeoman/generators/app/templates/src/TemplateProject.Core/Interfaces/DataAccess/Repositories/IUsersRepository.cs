using System.Collections.Generic;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Core.Interfaces.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        IList<IUser> GetAll(UsersFilter filter = null);

        IUser GetById(int id);

        IUser Get(string nameOrEMail, string passwordEncrypted);

        IList<IUser> GetByNameOrEMail(string name, string eMail);

        IUser Add(IUser user);

        IUser Update(IUser user);

        void DeleteById(int id);        
    }
}