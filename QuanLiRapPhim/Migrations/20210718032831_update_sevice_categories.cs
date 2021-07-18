using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update_sevice_categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "SeviceCategories",
                newName: "IsDelete");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2b9651bf-c9a4-4e50-a400-0d4aec83f4b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3727f993-2a7e-435a-b9fa-601cffcd4908");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b9bc8fce-7a4a-464e-8192-2e54ece57941");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcc7d5c9-6226-4204-a425-c923de057b51", "AGwlp9PILfll4ch3g1l9CM+s5GNb+1eRdq5GYn288R01gLKhXDKAUZAYrZCb8+mOlA==", "f9ffd4ac-70d8-4ac3-8480-e395c1e17cab" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "SeviceCategories",
                newName: "IsDeleted");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sevices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "69524e8e-0aa9-437f-ae38-e1a6d4c45291");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bb1d6a6a-3cdf-47c1-aa78-ed7a446202a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6a162018-1e8f-4028-a207-5232f695315d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d053acb-482c-4e5b-b958-c9b3b1affaec", "AD1ZG+gtyPh4vmi6mjfiiMqv5qQH7zEihW6ydK3hWY5BiMWR5LKqsPTTk/k5+/w3Og==", "3f3c1d01-cef5-429b-a0be-a22f504034ba" });
        }
    }
}
