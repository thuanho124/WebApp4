using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementWithAuthen.Migrations
{
    public partial class AddColumnAvailableQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Book",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Book");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Book",
                nullable: false,
                defaultValue: false);
        }
    }
}
