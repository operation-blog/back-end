using Microsoft.EntityFrameworkCore.Migrations;

namespace GF.DAL.Migrations
{
    public partial class BlogsCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogUsers_Users_UserId",
                table: "BlogUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUsers_Users_BlogId",
                table: "BlogUsers",
                column: "BlogId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogUsers_Users_BlogId",
                table: "BlogUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUsers_Users_UserId",
                table: "BlogUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
