using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class updateticket2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Tickets",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4a6a69fc-9916-443b-b582-52e9e44266e2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6eb3e5ce-c25e-4029-8a87-ab13bdb908dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1f8b02a1-351b-4cd2-8904-e3c5b3da2b27");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54fd7974-2f16-4f72-a3f2-f2961dca959d", "AKtZYUTtBEFioa1T8O8ESQHIGL7DTuNpg36kUCOajQRuqGYhFKC4hfUGi8QvbOy+5A==", "bae0be80-bd5a-4e77-a7fe-f910a8f020ce" });
        }
    }
}
