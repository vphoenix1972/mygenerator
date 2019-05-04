using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace <%= projectNamespace %>.Utils.EntityFrameworkCore
{
    public class DbContextWithSaveChangesHooks : DbContext
    {
        private readonly IList<Action> _hooks = new List<Action>();

        public DbContextWithSaveChangesHooks()
        {

        }

        public DbContextWithSaveChangesHooks(DbContextOptions options) :
            base(options)
        {

        }

        public DbSet<UserDataModel> Users { get; set; }

        public DbSet<UserRoleDataModel> UserRoles { get; set; }

        public DbSet<UserRoleUserDataModel> UserRoleUsers { get; set; }

        public DbSet<RefreshTokenDataModel> RefreshTokens { get; set; }

        public void AddSaveChangesHook(Action func)
        {
            _hooks.Add(func);
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            OnSaveChangesExecuted();

            return result;
        }


            SetupUserToUserRoleRelationship(mb);

            SetupUserToRefreshTokenRelationship(mb);
        }

        private void SetupUserToUserRoleRelationship(ModelBuilder mb)
        {
            mb.Entity<UserRoleUserDataModel>()
                .HasKey(t => new {t.UserId, t.RoleId});

            mb.Entity<UserRoleUserDataModel>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserUserRoles)
                .HasForeignKey(e => e.UserId);

            mb.Entity<UserRoleUserDataModel>()
                .HasOne(e => e.Role)
                .WithMany(e => e.UserRoleUsers)
                .HasForeignKey(e => e.RoleId);
        }

        private void SetupUserToRefreshTokenRelationship(ModelBuilder mb)
        {
            mb.Entity<UserDataModel>()
                .HasMany(e => e.RefreshTokens)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        private void OnSaveChangesExecuted()
        {
            foreach (var hook in _hooks)
            {
                hook.Invoke();
            }

            _hooks.Clear();
        }
    }
}
