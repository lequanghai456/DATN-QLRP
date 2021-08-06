using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class updatecategorysevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a627060f-6bc8-4b09-a827-546247bb9c1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8dbd463a-52ed-40fe-bd51-5c733b6b3990");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "64c2e124-8352-4ec6-a283-380ff03634b7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7af0cd3-57b7-404b-b4ec-504c24409d77", "AEJZgYNv4ICzogYYonmwL+Fr/zC/0Cwu1+jHWlkZarKv7aiPxS2imr/V16BShswr6w==", "36d5cb38-b61c-46ce-9ec9-33d6f5309d9e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8cb021c3-4fb0-4625-bf56-028d16a1b4d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e8c23082-8f37-4ac5-a335-f0fa95384cdf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2df9c9de-ee06-4b55-80ab-2e436f9006a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38bdb494-6939-454a-bde5-032078513f2c", "AEwLiwnVFR5/XhqXZf3km1/haiEW5W/yFHisg+/KlF15ACXkVxL+emRnOGKcXZW8lg==", "bddc5ed6-ca7f-4ebc-86d3-0eb2e3d76f03" });
        }
    }
}
