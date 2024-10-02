using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderingBookApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorNotes",
                table: "Articles",
                newName: "CuratorNote");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiscAuthor",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "MiscAuthor",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "CuratorNote",
                table: "Articles",
                newName: "AuthorNotes");
        }
    }
}
