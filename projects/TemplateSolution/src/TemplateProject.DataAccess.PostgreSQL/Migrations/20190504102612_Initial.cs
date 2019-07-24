using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TemplateProject.DataAccess.PostgreSQL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    PasswordEncrypted = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    ExpiresUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleUsers", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoleUsers_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleUsers_RoleId",
                table: "UserRoleUsers",
                column: "RoleId");

            SeedData(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "UserRoleUsers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }

        private void SeedData(MigrationBuilder mb)
        {
            SeedTodoItems(mb);

            SeedUsers(mb);
        }

        private void SeedTodoItems(MigrationBuilder mb)
        {
            for (var i = 0; i < 100; i++)
                mb.Sql($@"INSERT INTO ""TodoItems"" (""Name"") VALUES ('Item {i + 1}')");
        }

        private void SeedUsers(MigrationBuilder mb)
        {
            mb.Sql(@"INSERT INTO ""UserRoles"" (""Name"") VALUES ('admin'), ('user');");
            mb.Sql(@"INSERT INTO ""Users"" (""Name"", ""EMail"", ""PasswordEncrypted"") VALUES ('admin', 'admin@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055'), ('user', 'user@gmail.com', 'c4ca4238a0b923820dcc509a6f75849b');");
            
            mb.Sql(@"DO $$
                     DECLARE 
                         adminId integer;
                         userId integer;
                         adminRoleId integer;
                         userRoleId integer;
                     BEGIN
                         SELECT ""Id"" INTO adminId FROM ""Users"" WHERE ""Name"" = 'admin';
                         SELECT ""Id"" INTO userId FROM ""Users"" WHERE ""Name"" = 'user';
                         SELECT ""Id"" INTO adminRoleId FROM ""UserRoles"" WHERE ""Name"" = 'admin';
                         SELECT ""Id"" INTO userRoleId FROM ""UserRoles"" WHERE ""Name"" = 'user';

                         INSERT INTO ""UserRoleUsers"" (""UserId"", ""RoleId"") VALUES
                         (adminId, adminRoleId),
                         (adminId, userRoleId),
                         (userId, userRoleId);
    
                     END$$;");
        }
    }
}
