using Microsoft.EntityFrameworkCore.Migrations;

namespace GF.DAL.Migrations
{
    public partial class Blogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BlogAuthors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    BlogID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogAuthors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogAuthors_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogAuthors_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogUser",
                columns: table => new
                {
                    AuthorsID = table.Column<int>(type: "int", nullable: false),
                    BlogsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogUser", x => new { x.AuthorsID, x.BlogsID });
                    table.ForeignKey(
                        name: "FK_BlogUser_Blogs_BlogsID",
                        column: x => x.BlogsID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogUser_Users_AuthorsID",
                        column: x => x.AuthorsID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogAuthors_BlogID",
                table: "BlogAuthors",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogAuthors_UserID",
                table: "BlogAuthors",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogUser_BlogsID",
                table: "BlogUser",
                column: "BlogsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogAuthors");

            migrationBuilder.DropTable(
                name: "BlogUser");

            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
