using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDelete", "Name", "NormalizedName" },
                values: new object[] { 1, "4836867d-bfa0-43da-b1b1-b08babe6ddda", false, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDelete", "Name", "NormalizedName" },
                values: new object[] { 2, "9de713b3-cfdf-4a9d-9c90-0661de0537d0", false, "manager movie", "MANAGER MOVIE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDelete", "Name", "NormalizedName" },
                values: new object[] { 3, "b4ff7959-7b17-4fc9-b95f-038095647ff6", false, "staff", "STAFF" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Img", "IsDelete", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "e7409a49-c487-4ee9-a7c0-ee58da5c95a8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0306181100@caothang.edu.vn", false, "Hồ Gia Bảo", "admin.img", false, false, null, null, "admin", "ALbr3Vlfz3ZPC5Hw40NJ9yv2W04J/RuEhCwHVoiDpWco/9E8rP6l//qqFrnVHxfqow==", null, false, 1, "53be1fb4-aa88-42fb-a3fa-32a36aa7d531", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

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
