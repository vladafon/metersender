using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetersSender.DataAccess.Migrations
{
    public partial class RemoveConfigJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigJson",
                table: "Services");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfigJson",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
