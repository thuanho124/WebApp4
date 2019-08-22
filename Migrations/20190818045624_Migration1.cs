using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementWithAuthen.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentPhoneNumber",
                table: "Student",
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StudentPhoneNumber",
                table: "Student",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
