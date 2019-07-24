using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace <%= projectNamespace %>.DataAccess.SQLServer.Migrations
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            SeedData(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }

        private void SeedData(MigrationBuilder mb)
        {
            SeedTodoItems(mb);
        }

        private void SeedTodoItems(MigrationBuilder mb)
        {
            for (var i = 0; i < 100; i++)
                mb.Sql($@"INSERT INTO ""TodoItems"" (""Name"") VALUES ('Item {i + 1}')");
        }
    }
}
