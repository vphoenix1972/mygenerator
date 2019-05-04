﻿using Microsoft.EntityFrameworkCore;
using TemplateProject.DataAccess.SQLite.RefreshTokens;
using TemplateProject.DataAccess.SQLite.TodoItems;
using TemplateProject.DataAccess.SQLite.Users;
using TemplateProject.Utils.EntityFrameworkCore;

namespace TemplateProject.DataAccess.SQLite
{
    public sealed class ApplicationDbContext : DbContextWithSaveChangesHooks
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        public DbSet<TodoItemDataModel> TodoItems { get; set; }

        public DbSet<UserDataModel> Users { get; set; }

        public DbSet<UserRoleDataModel> UserRoles { get; set; }

        public DbSet<UserRoleUserDataModel> UserRoleUsers { get; set; }

        public DbSet<RefreshTokenDataModel> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            SetupUserToUserRoleRelationship(mb);

            SetupUserToRefreshTokenRelationship(mb);
        }

        private void SetupUserToUserRoleRelationship(ModelBuilder mb)
        {
            mb.Entity<UserRoleUserDataModel>()
                .HasKey(t => new { t.UserId, t.RoleId });

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
        }
    }
}
