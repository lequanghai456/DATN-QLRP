using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class updatMoviePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShowTimes");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Movies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "73894806-d493-48b4-947f-f1d159614f50");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f8a51663-7b65-4540-8255-a679f06d3131");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8a029b10-03e7-4b32-b9ae-0a6e739460ff");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab186e63-a6ff-44de-82a7-15a2e6b887f8", "AHrMxG5kcwcLmrz7qn6VrmZwyh/eyGJ3PwuQ/Jo9JOVkN6LkbvBcw+ix9sS4IRstVA==", "74b60b3b-6f1b-4014-980a-ceefbffc53da" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Movies");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShowTimes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2a2265ce-c648-4e34-be31-e4cc5a784a06");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "50c9af08-3ef7-4f0d-aa2e-9ea6a72b8e15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "91740d39-8b47-480f-9c65-c87aaec2c729");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "486b8d89-6d0d-47b5-b16f-7355bced8469", "AHvAq3G17ZubqQ8zRUEg0I4tkKCgKIsDHEfeY8qx9UK3pbZuDpQcOgvSnLehXcBGHg==", "2645f549-6cac-4f0a-83e2-73d60dae740d" });
        }
    }
}
