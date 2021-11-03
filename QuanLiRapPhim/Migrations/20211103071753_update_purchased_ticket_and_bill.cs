using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update_purchased_ticket_and_bill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPurchased",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPurchased",
                table: "Bills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "00735db5-3e36-4069-8704-710dd7f476ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ab89281d-8425-4132-b19e-7de910b1066d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cfd4d94-5d5a-42a0-b994-f44aba114417", "AFGJ9NWPYyocQ1nRYPEfGAqrzjoGQHZ1ehh2lciFx60ky+2xFVALt66Bc9Puu/23+A==", "cda907bc-85a0-47b0-8118-4ffd67cc6244" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d5f5520-7aca-474a-9523-7375807872dc", "APMvutPVqwwC6OLQuC/nUWU4dfPIOwD0FBKoRblsTpq0ijxm8vjrT1ehx9eVVJbY4Q==", "cdc0e105-62e5-44d5-88bd-f6bf9b42b3fb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPurchased",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsPurchased",
                table: "Bills");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "99635a72-f73e-40de-9d1c-22b8b11c746c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "14be720c-0a2a-4af1-926c-382938bde83d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b497f97-be69-4220-aaa9-d78de64aeae5", "AJfS1Gwqp/ZQQwtvXIWnEOiTlyyFz/CwzCJzeSpl6xRM4JCGrSTWGjxXRa98gVMU4w==", "6c527002-4dca-4f15-997f-128f3fd70dde" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dffdae2-b647-4791-9323-367b29d701ee", "ANqZeh9tJhaFVLCT0qamj+r7uOLAVhYbwGcyyTfiL6AvmPvXmdhQn7BAM0iey9qTJw==", "a9922bf7-997c-4490-97c5-a1ccf767b552" });
        }
    }
}
