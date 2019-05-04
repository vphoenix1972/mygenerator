﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TemplateProject.DataAccess.SQLite;

namespace TemplateProject.DataAccess.SQLite.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190504105729_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("TemplateProject.DataAccess.SQLite.RefreshTokens.RefreshTokenDataModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpiresUtc");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("TemplateProject.DataAccess.SQLite.TodoItems.TodoItemDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("TemplateProject.DataAccess.SQLite.Users.UserDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EMail");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordEncrypted");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TemplateProject.DataAccess.SQLite.Users.UserRoleDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("TemplateProject.DataAccess.SQLite.Users.UserRoleUserDataModel", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoleUsers");
                });

            modelBuilder.Entity("TemplateProject.DataAccess.SQLite.RefreshTokens.RefreshTokenDataModel", b =>
                {
                    b.HasOne("TemplateProject.DataAccess.SQLite.Users.UserDataModel", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TemplateProject.DataAccess.SQLite.Users.UserRoleUserDataModel", b =>
                {
                    b.HasOne("TemplateProject.DataAccess.SQLite.Users.UserRoleDataModel", "Role")
                        .WithMany("UserRoleUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TemplateProject.DataAccess.SQLite.Users.UserDataModel", "User")
                        .WithMany("UserUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
