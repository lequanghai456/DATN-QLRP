using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update_sevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFood",
                table: "Sevices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BillDetails",
                type: "nvarchar(max)",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFood",
                table: "Sevices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BillDetails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6e5ab9df-1705-41bf-9c8e-c43c11c5ea73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2724f1e2-3ca4-4b58-bfae-1374a7bf1c07");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b525b4d5-0fd5-4c34-b05b-f81cbf8e3ed4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "416e2c95-c436-4f86-9b37-319130146ba8", "AKzfBQIMHd646Jm/8cfwVnoshji856Q3EF37Vb3phmvQnYLz6/3pFLCPV6PLXx+hcw==", "c9adcc87-176a-4ad7-89b2-0e77ab8f96c6" });
        }
    }
}
