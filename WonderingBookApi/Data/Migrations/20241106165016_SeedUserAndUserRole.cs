using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WonderingBookApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserAndUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "Fullname", "IsAdmin", "LastActiveAt", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3bf051e7-cc4b-4b46-bff6-b3c982e51ecd", 0, "0ba16f7a-6406-4dea-b0a2-98252ba9180f", new DateTime(2024, 10, 26, 13, 56, 56, 0, DateTimeKind.Unspecified), "chienquyetsthang@gmail.com", false, "Trần Quyết Chiến", false, new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(7003), false, null, new DateTime(2024, 10, 26, 13, 56, 56, 0, DateTimeKind.Unspecified), "CHIENQUETSTHANG@GMAIL.COM", "CHIENQUETSTHANG", "AQAAAAIAAYagAAAAECxtILI8M8NNbIKk1SARv2l8niXqUXeHAjdd+U6s3Z88PI671uQL0vCJdgOhACDgBQ==", null, false, "FQ3GKA3ESFQ65WY6COZSY2NUNMJBJJCK", false, "chienquyetsthang" },
                    { "3d006212-a50c-45d5-9368-b8d3c0548de3", 0, "17b82427-bb11-458e-a9bd-96bd0f8ce73f", new DateTime(2024, 10, 23, 1, 57, 10, 0, DateTimeKind.Unspecified), "khanhgiahaika3@gmail.com", false, "Bùi Gia Khánh", false, new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(6990), false, null, new DateTime(2024, 10, 23, 1, 57, 10, 0, DateTimeKind.Unspecified), "KHANHGIAHAIKA3@GMAIL.COM", "KHANHGIAHAIKA3", "AQAAAAIAAYagAAAAEOInVfBvutBd8S8U2Ck8h2U3RHdD4EcHixqFx4djMr/io5hJ2kHThCmZ4R+gyZZWQw==", null, false, "5LVH7X6TASZOU4LISF6OD32DCGAIE52J", false, "khanhgiahaika3" },
                    { "3fc33030-5b15-481a-9ca6-ebb458b0e08c", 0, "404ea804-cbb9-4086-8f5a-25a77be98934", new DateTime(2024, 10, 21, 3, 32, 31, 0, DateTimeKind.Unspecified), "tuanhoang333@gmail.com", true, "Hoàng Huy Tuấn", false, new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(7023), false, null, new DateTime(2024, 10, 21, 3, 32, 47, 0, DateTimeKind.Unspecified), "TUANHOANG333@GMAIL.COM", "TUANHOANG333", "AQAAAAIAAYagAAAAEIRCZ/X2WM79M5kVq9c3L15pZrEvN/TTUEQ+H++Wd+gNuewMropdS1BJ47imojZR+Q==", null, false, "M5WLBKZOVBWHW2VWBW7IVYOPTFM7UIJR", false, "tuanhoang333" },
                    { "41d26d4e-d471-4e9c-b1fc-8b01e73218b3", 0, "072a38e7-3f28-40a3-b37f-278543217c7f", new DateTime(2024, 10, 22, 14, 45, 19, 0, DateTimeKind.Unspecified), "hoangtran8386@gmail.com", false, "Trần Huy Hoàng", false, new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(7013), false, null, new DateTime(2024, 10, 22, 14, 45, 19, 0, DateTimeKind.Unspecified), "HOANGTRAN8386@GMAIL.COM", "HOANGTRAN8386", "AQAAAAIAAYagAAAAEJCRqT2rhrpnh0B7xqxFZTFijjbXl/4i3/jQKkrr6uyIdYcLk67oAUdRmOvmkUdBnw==", null, false, "JFIALES6WXZYQRDRMSS4CUBPEM2ALTXJ", false, "hoangtran8386" },
                    { "4b0fe916-d76e-4182-a447-4893480c6b4c", 0, "8da8cab1-ee6d-4d24-9984-001ef8139ea3", new DateTime(2024, 10, 25, 13, 41, 1, 0, DateTimeKind.Unspecified), "giangthhe170978@fpt.edu.vn", false, "17 Tran Hoang Giang K17", true, new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(6981), false, null, new DateTime(2024, 10, 25, 13, 41, 1, 0, DateTimeKind.Unspecified), "GIANGTHHE170978@FPT.EDU.VN", "GIANGTHHE170978", null, null, false, "QVQX4RBEDDPFMPWUJYH45JH7YJFFTAOS", false, "giangthhe170978" },
                    { "7a9df144-5fe3-4adb-b60c-0e309df83bd7", 0, "7b21a2d8-976f-4ce8-8c81-c9a9578faace", new DateTime(2024, 10, 24, 5, 14, 16, 0, DateTimeKind.Unspecified), "hungtdhe171201@fpt.edu.vn", false, "Tran Duc Hung (K17 HL)", true, new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(6966), false, null, new DateTime(2024, 10, 24, 5, 14, 16, 0, DateTimeKind.Unspecified), "HUNGTDHE171201@FPT.EDU.VN", "HUNGTDHE171201", null, null, false, "S75KO3CFMZXMHCVSWCS4T6JY3IEZO4LD", false, "hungtdhe171201" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f1de4cb5-7f7c-4ba9-9453-22ec3840984b", "3bf051e7-cc4b-4b46-bff6-b3c982e51ecd" },
                    { "03482d4b-c825-4ae8-a1f1-26e98f3b8a5b", "3d006212-a50c-45d5-9368-b8d3c0548de3" },
                    { "fcd4a4b5-d492-4290-8c45-8c94b7f8d689", "3fc33030-5b15-481a-9ca6-ebb458b0e08c" },
                    { "2281c643-65a2-4fd6-83bc-ae240794b875", "41d26d4e-d471-4e9c-b1fc-8b01e73218b3" },
                    { "1bfa4d50-b885-48b9-835a-f47ad854046b", "4b0fe916-d76e-4182-a447-4893480c6b4c" },
                    { "1bfa4d50-b885-48b9-835a-f47ad854046b", "7a9df144-5fe3-4adb-b60c-0e309df83bd7" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f1de4cb5-7f7c-4ba9-9453-22ec3840984b", "3bf051e7-cc4b-4b46-bff6-b3c982e51ecd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "03482d4b-c825-4ae8-a1f1-26e98f3b8a5b", "3d006212-a50c-45d5-9368-b8d3c0548de3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fcd4a4b5-d492-4290-8c45-8c94b7f8d689", "3fc33030-5b15-481a-9ca6-ebb458b0e08c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2281c643-65a2-4fd6-83bc-ae240794b875", "41d26d4e-d471-4e9c-b1fc-8b01e73218b3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1bfa4d50-b885-48b9-835a-f47ad854046b", "4b0fe916-d76e-4182-a447-4893480c6b4c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1bfa4d50-b885-48b9-835a-f47ad854046b", "7a9df144-5fe3-4adb-b60c-0e309df83bd7" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bf051e7-cc4b-4b46-bff6-b3c982e51ecd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d006212-a50c-45d5-9368-b8d3c0548de3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3fc33030-5b15-481a-9ca6-ebb458b0e08c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41d26d4e-d471-4e9c-b1fc-8b01e73218b3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b0fe916-d76e-4182-a447-4893480c6b4c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a9df144-5fe3-4adb-b60c-0e309df83bd7");
        }
    }
}
