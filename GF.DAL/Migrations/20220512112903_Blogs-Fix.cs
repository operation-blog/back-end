using Microsoft.EntityFrameworkCore.Migrations;

namespace GF.DAL.Migrations
{
    public partial class BlogsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogAuthors_Blogs_BlogID",
                table: "BlogAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogAuthors_Users_UserID",
                table: "BlogAuthors");

            migrationBuilder.DropTable(
                name: "BlogUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogAuthors",
                table: "BlogAuthors");

            migrationBuilder.DropIndex(
                name: "IX_BlogAuthors_BlogID",
                table: "BlogAuthors");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "BlogAuthors");

            migrationBuilder.RenameTable(
                name: "BlogAuthors",
                newName: "BlogUsers");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "BlogUsers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "BlogID",
                table: "BlogUsers",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogAuthors_UserID",
                table: "BlogUsers",
                newName: "IX_BlogUsers_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BlogUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "BlogUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogUsers",
                table: "BlogUsers",
                columns: new[] { "BlogId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUsers_Blogs_UserId",
                table: "BlogUsers",
                column: "UserId",
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
                name: "FK_BlogUsers_Blogs_UserId",
                table: "BlogUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogUsers_Users_UserId",
                table: "BlogUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogUsers",
                table: "BlogUsers");

            migrationBuilder.RenameTable(
                name: "BlogUsers",
                newName: "BlogAuthors");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BlogAuthors",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "BlogAuthors",
                newName: "BlogID");

            migrationBuilder.RenameIndex(
                name: "IX_BlogUsers_UserId",
                table: "BlogAuthors",
                newName: "IX_BlogAuthors_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "BlogAuthors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BlogID",
                table: "BlogAuthors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "BlogAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogAuthors",
                table: "BlogAuthors",
                column: "ID");

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
                name: "IX_BlogUser_BlogsID",
                table: "BlogUser",
                column: "BlogsID");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogAuthors_Blogs_BlogID",
                table: "BlogAuthors",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogAuthors_Users_UserID",
                table: "BlogAuthors",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
