using Microsoft.EntityFrameworkCore.Migrations;

namespace GF.DAL.Migrations
{
    public partial class BlogsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogUsers_Blogs_UserId",
                table: "BlogUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogUsers_Users_BlogId",
                table: "BlogUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogUsers",
                table: "BlogUsers");

            migrationBuilder.DropIndex(
                name: "IX_BlogUsers_UserId",
                table: "BlogUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogUsers",
                table: "BlogUsers",
                columns: new[] { "UserId", "BlogId" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogUsers_BlogId",
                table: "BlogUsers",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUsers_Blogs_BlogId",
                table: "BlogUsers",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUsers_Users_UserId",
                table: "BlogUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogUsers_Blogs_BlogId",
                table: "BlogUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogUsers_Users_UserId",
                table: "BlogUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogUsers",
                table: "BlogUsers");

            migrationBuilder.DropIndex(
                name: "IX_BlogUsers_BlogId",
                table: "BlogUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogUsers",
                table: "BlogUsers",
                columns: new[] { "BlogId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogUsers_UserId",
                table: "BlogUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUsers_Blogs_UserId",
                table: "BlogUsers",
                column: "UserId",
                principalTable: "Blogs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUsers_Users_BlogId",
                table: "BlogUsers",
                column: "BlogId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
