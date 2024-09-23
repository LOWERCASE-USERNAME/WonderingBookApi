using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderingBookApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixArticleAndSavedIdeas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserId1",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedIdeas_AspNetUsers_UserId1",
                table: "SavedIdeas");

            migrationBuilder.DropIndex(
                name: "IX_SavedIdeas_UserId1",
                table: "SavedIdeas");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserId1",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "SavedIdeas");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SavedIdeas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_SavedIdeas_UserId",
                table: "SavedIdeas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedIdeas_AspNetUsers_UserId",
                table: "SavedIdeas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedIdeas_AspNetUsers_UserId",
                table: "SavedIdeas");

            migrationBuilder.DropIndex(
                name: "IX_SavedIdeas_UserId",
                table: "SavedIdeas");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SavedIdeas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "SavedIdeas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Articles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedIdeas_UserId1",
                table: "SavedIdeas",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId1",
                table: "Articles",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserId1",
                table: "Articles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedIdeas_AspNetUsers_UserId1",
                table: "SavedIdeas",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
