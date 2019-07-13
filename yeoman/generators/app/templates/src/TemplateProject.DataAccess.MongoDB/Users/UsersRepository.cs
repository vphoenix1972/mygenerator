using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using MongoDB.Driver;
using <%= csprojName %>.Core.Domain;
using <%= csprojName %>.Core.Interfaces.DataAccess.Repositories;
using <%= csprojName %>.Utils.Factories;

namespace <%= csprojName %>.DataAccess.MongoDB.Users
{
    internal sealed class UsersRepository : RepositoryBase<IUser, User, UserDataModel>,
        IUsersRepository
    {
        public UsersRepository(IMongoDatabase db, IMapper mapper, IFactory<User> usersFactory)
            : base("Users", db, mapper, usersFactory)
        {
        }

        public IList<IUser> GetAll(UsersFilter filter = null)
        {
            var mongoFilter = BuildQuery(filter);

            var result = Collection.Find(mongoFilter).ToList();

            return result.Select(Map).ToList();
        }

        public IUser Get(string nameOrEMail, string passwordEncrypted)
        {
            var userDataModel = Collection.Find(x => (x.Name == nameOrEMail || x.EMail == nameOrEMail) && x.PasswordEncrypted == passwordEncrypted)
                .FirstOrDefault();
            if (userDataModel == null)
                return null;

            return Map(userDataModel);
        }

        public IList<IUser> GetByNameOrEMail(string name, string eMail)
        {
            var result = Collection.Find(x => x.Name == name || x.EMail == eMail).ToList();

            return result.Select(Map).ToList();
        }

        public IUser Add(IUser user) => AddOrUpdate(user);

        public IUser Update(IUser user)
        {
            var exisitng = GetById(user.Id);
            if (exisitng == null)
                throw new ArgumentException($"User with id '{user.Id}' doesn't exist in database");

            return AddOrUpdate(user);
        }

        private FilterDefinition<UserDataModel> BuildQuery(UsersFilter filter)
        {
            var result = FilterDefinition<UserDataModel>.Empty;

            if (filter == null)
                return result;

            var builder = Builders<UserDataModel>.Filter;

            if (filter.ExcludeUserRoleNames != null)
            {
                result &= builder.Nin(x => x.Name, filter.ExcludeUserRoleNames);
            }

            return result;
        }
    }
}
