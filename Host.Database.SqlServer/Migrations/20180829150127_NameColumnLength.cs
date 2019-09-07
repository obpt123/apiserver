using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Host.Database.SqlServer.Migrations
{
    public partial class NameColumnLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    CreateUser = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: true),
                    Description = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
