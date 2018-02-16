using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess.Repositories;
using TemplateProject.DataAccess.Models;
using TemplateProject.Utils.Factories;

namespace TemplateProject.DataAccess.Repositories
{
    public sealed class UsersRepository :
        IUsersRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IFactory<User> _usersFactory;

        public UsersRepository(ApplicationDbContext db, IFactory<User> usersFactory)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            if (usersFactory == null)
                throw new ArgumentNullException(nameof(usersFactory));

            _db = db;
            _usersFactory = usersFactory;
        }

        public IUser GetById(int id)
        {
            var userDataModel = _db.Users
                .Include(e => e.UserUserRoles)
                .ThenInclude(e => e.Role)
                .SingleOrDefault(e => e.Id == id);

            return Map(userDataModel);
        }

        public IUser Get(string nameOrEMail, string password)
        {
            var userDataModel = _db.Users
                .Include(e => e.UserUserRoles)
                .ThenInclude(e => e.Role)
                .FirstOrDefault(e => (e.Name == nameOrEMail || e.EMail == nameOrEMail) &&
                                     e.Password == password);
            if (userDataModel == null)
                return null;

            return Map(userDataModel);
        }

        public IList<IUser> GetByNameOrEMail(string name, string eMail)
        {
            var result = _db.Users
                .Include(e => e.UserUserRoles)
                .ThenInclude(e => e.Role)
                .Where(e => e.Name == name || e.EMail == eMail)
                .ToList();

            return result.Select(Map).ToList();
        }

        public IUser Add(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userDataModel = new UserDataModel();

            Map(user, userDataModel);

            foreach (var role in user.Roles)
            {
                var roleDataModel = _db.UserRoles
                    .FirstOrDefault(e => e.Name == role.Name);
                if (roleDataModel == null)
                    throw new ArgumentException($"Role '{role.Name}' does not exist in database");

                var link = new UserRoleUserDataModel()
                {
                    User = userDataModel,
                    Role = roleDataModel
                };

                _db.UserRoleUsers.Add(link);
            }

            _db.Users.Add(userDataModel);

            _db.AddSaveChangesHook(() => user.Id = userDataModel.Id);

            return user;
        }

        private IUser Map(UserDataModel source)
        {
            var dest = _usersFactory.Create();

            Map(source, dest);

            return dest;
        }

        private void Map(UserDataModel source, User dest)
        {
            dest.Id = source.Id;
            dest.Name = source.Name;
            dest.EMail = source.EMail;
            dest.Password = source.Password;
            dest.Roles = source.UserUserRoles.Select(e => new UserRole() {Name = e.Role.Name} as IUserRole).ToList();
        }

        private void Map(IUser source, UserDataModel dest)
        {
            dest.Name = source.Name;
            dest.EMail = source.EMail;
            dest.Password = source.Password;
        }
    }
}