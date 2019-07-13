﻿using System.Collections.Generic;
using TemplateProject.Core.Domain;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        IList<IUser> GetAll(UsersFilter filter = null);

        IUser GetById(string id);

        IUser Get(string nameOrEMail, string passwordEncrypted);

        IList<IUser> GetByNameOrEMail(string name, string eMail);

        IUser Add(IUser user);

        IUser Update(IUser user);

        void DeleteById(string id);
    }
}