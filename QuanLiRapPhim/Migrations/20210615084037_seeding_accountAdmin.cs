using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class seeding_accountAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "672d4d6c-b378-442b-95b7-407b83535b0b", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Img", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "398ce8ed-5b65-47fa-b1c5-9ac0e3ddf602", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0306181100@caothang.edu.vn", false, "Hồ Gia Bảo", "admin.img", false, null, null, null, "AQAAAAEAACcQAAAAEF9u1bOqWI0jVx9W90CBKSC4tesC72Ddrk3XnwUMBXBIZ5JdeuqgIPGi0UHCQxIUXQ==", null, false, 1, null, false, "admin" });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
