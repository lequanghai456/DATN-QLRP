using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class model_seat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ed4d2b30-be60-4c26-944a-a44a0337cd8c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5737dfae-50a7-486d-949a-bf436f481487");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "dbf33b53-c2c9-4b5e-9815-14022f5b6466");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a929677-52ce-4bff-922b-c4e07e50a96c", "AOhFoENeAPMjltHLkWUBkRgPRJHxU1x/NqlyV37YjD/zuLertiQh+uEIJmP4OzehCw==", "1e16d41a-eb04-4e9e-94e4-1e0538601455" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Seats");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4836867d-bfa0-43da-b1b1-b08babe6ddda");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9de713b3-cfdf-4a9d-9c90-0661de0537d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b4ff7959-7b17-4fc9-b95f-038095647ff6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7409a49-c487-4ee9-a7c0-ee58da5c95a8", "ALbr3Vlfz3ZPC5Hw40NJ9yv2W04J/RuEhCwHVoiDpWco/9E8rP6l//qqFrnVHxfqow==", "53be1fb4-aa88-42fb-a3fa-32a36aa7d531" });
        }
    }
}
