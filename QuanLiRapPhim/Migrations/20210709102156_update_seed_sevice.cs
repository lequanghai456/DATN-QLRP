using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update_seed_sevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "12a6ecb5-e5e3-4283-91e0-5423e20cee87");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dcae7e69-1bb6-4d6b-b711-da8ac98a4f56");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "47befe46-c45b-42cd-bcee-760ef393f6c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6527c063-2fe8-4f2d-874b-0e227b18c315", "AFZqY+owznJZSM4IUQI7FkgrVe8FNU80hsf/Nlufjdl3eC47CpVGZLyjWcXxL0gz7Q==", "f36dcb00-d248-4287-b412-4613fcff3311" });

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsFood",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a22b6e21-2bd7-436f-a3e7-b23389a6fdac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "93904bc4-b00c-4732-888f-cea4f8007229");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0d2d3649-f6aa-423c-8f45-0c51bb6d0f90");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "852077f2-ae14-49dc-9bb6-45aba3c6a172", "ALgFncA9NJOtuoKn1iEH6jMaym6lAWZ4TTbugMEqH4ExowkCyARjwzgefgiZ+QGgfg==", "519521a3-a91b-48c3-ab50-1a2bd5e1f715" });

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsFood",
                value: false);
        }
    }
}
