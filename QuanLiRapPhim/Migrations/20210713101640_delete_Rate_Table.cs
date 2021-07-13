using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class delete_Rate_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Rates_RateId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Movies_RateId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RateId",
                table: "Movies");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RateId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cfe4b557-d14d-4183-b387-5cb2a3a896f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "65ec4011-2dfb-40d8-aeb7-e64196451984");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1b3949bf-856b-4b55-a3d7-478fd8eb71df");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ce37990-4348-436e-a04b-f3cd5f159261", "AIiUyy+dCW9m8LXVlrLSRngQD3QPxly6DQ4BlilPe3vQgMSsESxJcs99BTCSJNg3XA==", "d0d8e000-49dd-4dd5-8a2a-d7c03b4ff7a0" });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_RateId",
                table: "Movies",
                column: "RateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Rates_RateId",
                table: "Movies",
                column: "RateId",
                principalTable: "Rates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
