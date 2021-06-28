using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class createsevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Sevices",
                columns: new[] { "Id", "IsDelete", "Name", "Price" },
                values: new object[] { 1, false, "Bắp rang", 10000m });

            migrationBuilder.InsertData(
                table: "Sevices",
                columns: new[] { "Id", "IsDelete", "Name", "Price" },
                values: new object[] { 2, false, "CoCa", 10000m });

            migrationBuilder.InsertData(
                table: "Sevices",
                columns: new[] { "Id", "IsDelete", "Name", "Price" },
                values: new object[] { 3, false, "Pepsi", 10000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDelete", "Name", "NormalizedName" },
                values: new object[] { 1, "b67d78e1-beef-47a1-b40d-858786de7fef", false, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDelete", "Name", "NormalizedName" },
                values: new object[] { 2, "d29fee06-3108-4c28-a3d0-e5c9eab66174", false, "manager movie", "MANAGER MOVIE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDelete", "Name", "NormalizedName" },
                values: new object[] { 3, "789b60c8-8ebc-4ae2-b0ff-bf0391047531", false, "staff", "STAFF" });
        }
    }
}
