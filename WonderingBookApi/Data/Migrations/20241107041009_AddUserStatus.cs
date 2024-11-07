using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderingBookApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bf051e7-cc4b-4b46-bff6-b3c982e51ecd",
                columns: new[] { "LastActiveAt", "Status" },
                values: new object[] { new DateTime(2024, 11, 7, 11, 10, 6, 564, DateTimeKind.Local).AddTicks(8653), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d006212-a50c-45d5-9368-b8d3c0548de3",
                columns: new[] { "LastActiveAt", "Status" },
                values: new object[] { new DateTime(2024, 11, 7, 11, 10, 6, 564, DateTimeKind.Local).AddTicks(8643), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3fc33030-5b15-481a-9ca6-ebb458b0e08c",
                columns: new[] { "LastActiveAt", "Status" },
                values: new object[] { new DateTime(2024, 11, 7, 11, 10, 6, 564, DateTimeKind.Local).AddTicks(8674), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41d26d4e-d471-4e9c-b1fc-8b01e73218b3",
                columns: new[] { "LastActiveAt", "Status" },
                values: new object[] { new DateTime(2024, 11, 7, 11, 10, 6, 564, DateTimeKind.Local).AddTicks(8663), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b0fe916-d76e-4182-a447-4893480c6b4c",
                columns: new[] { "LastActiveAt", "Status" },
                values: new object[] { new DateTime(2024, 11, 7, 11, 10, 6, 564, DateTimeKind.Local).AddTicks(8633), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a9df144-5fe3-4adb-b60c-0e309df83bd7",
                columns: new[] { "LastActiveAt", "Status" },
                values: new object[] { new DateTime(2024, 11, 7, 11, 10, 6, 564, DateTimeKind.Local).AddTicks(8607), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bf051e7-cc4b-4b46-bff6-b3c982e51ecd",
                column: "LastActiveAt",
                value: new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(7003));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d006212-a50c-45d5-9368-b8d3c0548de3",
                column: "LastActiveAt",
                value: new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(6990));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3fc33030-5b15-481a-9ca6-ebb458b0e08c",
                column: "LastActiveAt",
                value: new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(7023));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41d26d4e-d471-4e9c-b1fc-8b01e73218b3",
                column: "LastActiveAt",
                value: new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(7013));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b0fe916-d76e-4182-a447-4893480c6b4c",
                column: "LastActiveAt",
                value: new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(6981));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a9df144-5fe3-4adb-b60c-0e309df83bd7",
                column: "LastActiveAt",
                value: new DateTime(2024, 11, 6, 23, 50, 15, 507, DateTimeKind.Local).AddTicks(6966));
        }
    }
}
