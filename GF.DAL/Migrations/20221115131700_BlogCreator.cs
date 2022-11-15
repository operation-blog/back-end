using Microsoft.EntityFrameworkCore.Migrations;

namespace GF.DAL.Migrations
{
    public partial class BlogCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficialCreatorID",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_OfficialCreatorID",
                table: "Blogs",
                column: "OfficialCreatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_OfficialCreatorID",
                table: "Blogs",
                column: "OfficialCreatorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_OfficialCreatorID",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_OfficialCreatorID",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "OfficialCreatorID",
                table: "Blogs");
        }
    }
}
