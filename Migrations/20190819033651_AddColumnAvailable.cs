using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementWithAuthen.Migrations
{
    public partial class AddColumnAvailable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Book",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Book");
        }
    }
}
