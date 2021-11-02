using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class updatemodelcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ec9fd79b-736b-4a50-9810-19ed828541c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5159a846-fbe5-4887-9e0e-643756f27fd0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b1f06639-ba2e-41fc-a207-392df9b1e3cd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "961e34d9-ab37-4f52-97b4-b51a712b96e4", "AP+9g49g4kaPlyC/4odnxkizZMMBVdmDBg/VDcVCJyoG098aVMSCtHG94KfIcnpM0A==", "a007f7c5-cc58-446e-9de9-5ce3590de3e3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "70996687-0659-4ec9-9562-292cd77494cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ed3b5017-c83e-4bdf-9c21-04963bec6e0a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9d45d3b3-e2c8-4365-82b9-031ceeadb77f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53381a11-fd8e-4518-a91d-4d2a019d90b9", "AGEbd4Gu2j8ce7yqqQm5bDvfecrwUtTIeA2+546C5BQwaUQZlOkHJIQVvlKkj/qZZQ==", "ff145e38-83f2-467c-b1d9-aaa73a85b3a5" });
        }
    }
}
