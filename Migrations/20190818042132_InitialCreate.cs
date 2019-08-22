using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementWithAuthen.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookName = table.Column<string>(maxLength: 60, nullable: false),
                    Author = table.Column<string>(maxLength: 30, nullable: true),
                    Edition = table.Column<string>(maxLength: 5, nullable: true),
                    ISBN = table.Column<long>(nullable: false),
                    Subject = table.Column<string>(maxLength: 15, nullable: true),
                    PublicDate = table.Column<DateTime>(nullable: false),
                    Format = table.Column<string>(maxLength: 10, nullable: false),
                    NumofPages = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "Librarian",
                columns: table => new
                {
                    LibrarianID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LibrarianFirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LibrarianLastName = table.Column<string>(maxLength: 30, nullable: false),
                    LibrarianEmail = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarian", x => x.LibrarianID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentFirstName = table.Column<string>(maxLength: 30, nullable: false),
                    StudentLastName = table.Column<string>(maxLength: 30, nullable: false),
                    StudentEmail = table.Column<string>(maxLength: 30, nullable: false),
                    StudentPhoneNumber = table.Column<int>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "RentedBook",
                columns: table => new
                {
                    RentedBookID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentID = table.Column<int>(nullable: false),
                    BookID = table.Column<int>(nullable: false),
                    RentDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentedBook", x => x.RentedBookID);
                    table.ForeignKey(
                        name: "FK_RentedBook_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentedBook_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentedBook_BookID",
                table: "RentedBook",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_RentedBook_StudentID",
                table: "RentedBook",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Librarian");

            migrationBuilder.DropTable(
                name: "RentedBook");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
