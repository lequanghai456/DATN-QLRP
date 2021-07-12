using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class themseeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Macs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2fe8caa9-e72b-4687-a78d-6f43339fa08b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ba7f6d78-1e5c-4403-8901-c6c1ceca567f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4f231dce-bfb8-4c3f-a1d0-f30b181e0861");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a46c944b-8747-40df-8735-820b05416eb1", "AP7Jka0wDj0IgZaWgfd59FASTImKlz/EHqKn5ori4mMk7u/3jNNWREELy4jEe1ByHw==", "7e52db0d-d6bf-4270-90f4-136c014faf73" });

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Bắp rang bơ", 30000m });

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Coca", 15000m });

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "Price" },
                values: new object[] { "PepSi", 15000m });

            migrationBuilder.InsertData(
                table: "Sevices",
                columns: new[] { "Id", "IsDelete", "Name", "Price" },
                values: new object[] { 4, false, "Bắp", 15000m });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "ConfirmEmail", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Img", "IsDelete", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "cd0df68e-4630-4df5-9fbd-dd8e6e96aeab", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0306181100@caothang.edu.vn", false, "Hồ Gia Bảo", "avatar.png", false, false, null, null, "HoBao", "AKCQ+TE+4y0tn4DgcqoaLuBLJSPzfzuJYMHD3oZHe+1dkNIJYdXfASXlkoL+tjiWvw==", null, false, "1795fd99-24eb-4daa-9515-bfb4f46d1190", false, "HoBao" });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "Date", "IsDelete", "Status", "TotalPrice", "UserId" },
                values: new object[] { 1, new DateTime(2020, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 30000m, 1 });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "Date", "IsDelete", "Status", "TotalPrice", "UserId" },
                values: new object[] { 2, new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 30000m, 1 });

            migrationBuilder.InsertData(
                table: "BillDetails",
                columns: new[] { "Id", "Amount", "BillId", "SeviceId", "UnitPrice" },
                values: new object[] { 1, 3, 1, 2, 4000m });

            migrationBuilder.InsertData(
                table: "BillDetails",
                columns: new[] { "Id", "Amount", "BillId", "SeviceId", "UnitPrice" },
                values: new object[] { 2, 3, 2, 1, 4000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BillDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BillDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sevices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Macs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

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

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Bắp rang", 10000m });

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Price" },
                values: new object[] { "CoCa", 10000m });

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Pepsi", 10000m });
        }
    }
}
