using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update_model_room1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d2a74ae2-92e9-46a9-bd91-b653a388a763");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c4b1f48a-343b-475f-9dc8-5dbb65c7216f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba7a6ce7-d853-4a2d-bd52-cabb66c3e2fe", "ANYoDIwYiz7Qp2UNvOP7qa6N1mMkZkekuZQV8C0pvb82qM5ucpcVThYhojxPM7zcpg==", "14dfa38c-ab84-460e-ade1-2ec257fee69b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e325527-ffa4-4a5e-8c57-6ffbfcbb8990", "AFNiUwJQ5hF4e/A1M4coPc5UdlNhZNGPYKNLRU6F0nGZLBVlCJXaqDjGizMXqHRRww==", "2b43b56c-c622-484e-8088-3840cbbe0557" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7e0cda90-9179-406d-806d-6fd72c7626c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8e51bece-f2ba-4a6c-bfe4-ca3bad8bcf46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4fff984-182a-4858-986e-feaee7bf49e0", "AOhUaWCxMgvma1/Lyath2/CDLE3x8QImPQ6mfAlDe2DEM6dkAag+Z4DScJUuevC8nA==", "4a930a8e-6017-41b3-9fee-5d6cbb361554" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0771fbfe-a5e3-4386-b295-7351950a2bc6", "AGPxUkfNjkCVLM6/XKZ0TWaxqCV881uxiKWy2EykiCMeTtHh++QndSpxHguWjfldog==", "48b031d5-8b3f-4271-874e-124f347cd6fb" });
        }
    }
}
