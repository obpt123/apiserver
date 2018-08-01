using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppService.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AppKey = table.Column<string>(nullable: true),
                    AppName = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    CustomCssUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInfos");
        }
    }
}
