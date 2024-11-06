using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WonderingBookApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03482d4b-c825-4ae8-a1f1-26e98f3b8a5b", null, "Moderator", "MODERATOR" },
                    { "1bfa4d50-b885-48b9-835a-f47ad854046b", null, "Admin", "ADMIN" },
                    { "2281c643-65a2-4fd6-83bc-ae240794b875", null, "PremiumUser", "PREMIUMUSER" },
                    { "f1de4cb5-7f7c-4ba9-9453-22ec3840984b", null, "ContentProvider", "CONTENTPROVIDER" },
                    { "fcd4a4b5-d492-4290-8c45-8c94b7f8d689", null, "RegularUser", "REGULARUSER" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "TopicId", "TopicName" },
                values: new object[,]
                {
                    { 1, "habit" },
                    { 2, "productivity" },
                    { 3, "mindfulness" },
                    { 4, "motivation" },
                    { 5, "personal-development" },
                    { 6, "success" },
                    { 7, "growth" },
                    { 8, "learning" },
                    { 9, "inspiration" },
                    { 10, "wellness" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03482d4b-c825-4ae8-a1f1-26e98f3b8a5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bfa4d50-b885-48b9-835a-f47ad854046b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2281c643-65a2-4fd6-83bc-ae240794b875");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1de4cb5-7f7c-4ba9-9453-22ec3840984b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcd4a4b5-d492-4290-8c45-8c94b7f8d689");

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: 10);
        }
    }
}
