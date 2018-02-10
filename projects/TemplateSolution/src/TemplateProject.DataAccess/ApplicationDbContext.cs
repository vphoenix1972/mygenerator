using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TemplateProject.DataAccess.Models;

namespace TemplateProject.DataAccess
{
    public sealed class ApplicationDbContext : DbContext
    {
        private readonly IList<Action> _hooks = new List<Action>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        public DbSet<TodoItemDataModel> TodoItems { get; set; }

        public DbSet<UserDataModel> Users { get; set; }

        public DbSet<UserRoleDataModel> UserRoles { get; set; }

        public DbSet<UserRoleUserDataModel> UserRoleUsers { get; set; }

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

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            SetupUserToUserRoleRelationShip(mb);
        }

        private void SetupUserToUserRoleRelationShip(ModelBuilder mb)
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