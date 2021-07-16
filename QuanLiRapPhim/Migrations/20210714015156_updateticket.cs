using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class updateticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4a6a69fc-9916-443b-b582-52e9e44266e2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6eb3e5ce-c25e-4029-8a87-ab13bdb908dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1f8b02a1-351b-4cd2-8904-e3c5b3da2b27");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54fd7974-2f16-4f72-a3f2-f2961dca959d", "AKtZYUTtBEFioa1T8O8ESQHIGL7DTuNpg36kUCOajQRuqGYhFKC4hfUGi8QvbOy+5A==", "bae0be80-bd5a-4e77-a7fe-f910a8f020ce" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "adcf7f5f-b005-40bf-bc63-f2f29af95829");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c2a28e66-92bd-49d3-84e1-c0067cff35a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ab1fe913-75ec-4f3d-8012-e4660b8d659e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "447bf63b-563f-4f25-9c22-483632a0f023", "ACtsNKe4xwKMdTCekb9PCcLekO/l/xzVb1pYuViIjZLAqEjRY87x/FoS4s9b1STdqg==", "0c3c0986-6711-4dff-934a-1f7bdf01c883" });
        }
    }
}
