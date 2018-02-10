using System;
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

        public IUser Get(string nameOrEMail, string password)
        {
            var userDataModel = _db.Users
                .Include(e => e.UserUserRoles)
                .ThenInclude(e => e.Role)
                .FirstOrDefault(e => (e.Name == nameOrEMail || e.EMail == nameOrEMail) &&
                                     e.Password == password);
            if (userDataModel == null)
                return null;

            var user = _usersFactory.Create();

            Map(userDataModel, user);

            return user;
        }

        private void Map(UserDataModel source, User dest)
        {
            dest.Id = source.Id;
            dest.Name = source.Name;
            dest.EMail = source.EMail;
            dest.Password = source.Password;
            dest.Roles = source.UserUserRoles.Select(e => new UserRole() {Name = e.Role.Name} as IUserRole).ToList();
        }
    }
}